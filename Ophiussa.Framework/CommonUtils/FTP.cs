using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace OphiussaFramework.CommonUtils {
    public struct DirectoryItem {
        public Uri BaseUri;

        public string AbsolutePath => string.Format("{0}", Name);

        public DateTime            DateCreated;
        public bool                IsDirectory;
        public string              Name;
        public List<DirectoryItem> Items;
    }

    public class FtpClient {
        private readonly int bufferSize = 2048;


        private readonly string         host;
        private readonly string         pass;
        private readonly string         user;
        private          FtpWebRequest  ftpRequest;
        private          FtpWebResponse ftpResponse;
        private          Stream         ftpStream;

        /* Construct Object */
        public FtpClient(string hostIP, string userName, string password) {
            host = hostIP;
            user = userName;
            pass = password;
        }

        /* Download File */
        public void download(string remoteFile, string localFile) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Get the FTP Server's Response Stream */
            ftpStream = ftpResponse.GetResponseStream();
            /* Open a File Stream to Write the Downloaded File */
            var localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[bufferSize];
            int    bytesRead  = ftpStream.Read(byteBuffer, 0, bufferSize);
            /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
            try {
                while (bytesRead > 0) {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Resource Cleanup */
            localFileStream.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }

        /* Upload File */
        public void upload(string remoteFile, string localFile) {
            try {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary  = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive  = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpRequest.GetRequestStream();
                /* Open a File Stream to Read the File for Upload */
                var localFileStream = new FileStream(localFile, FileMode.Open);
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int    bytesSent  = localFileStream.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                try {
                    while (bytesSent != 0) {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex2) {
                    throw ex2;
                }

                /* Resource Cleanup */
                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /* Delete File */
        public void delete(string deleteFile) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }

        /* Delete Dir*/
        public void deleteDir(string deleteFile) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }

        public string MoveFile(string source, string destination) {
            if (source == destination)
                return "";

            var uriSource      = new Uri(host + "/" + source,      UriKind.Absolute);
            var uriDestination = new Uri(host + "/" + destination, UriKind.Absolute);


            var targetUriRelative = uriSource.MakeRelativeUri(uriDestination);


            //perform rename
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + source);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            ftpRequest.Method     = WebRequestMethods.Ftp.Rename;
            ftpRequest.RenameTo   = Uri.UnescapeDataString(targetUriRelative.OriginalString);

            var response = (FtpWebResponse)ftpRequest.GetResponse();
            return "";
        }

        /* Rename File */
        public void rename(string currentFileNameAndPath, string newFileName) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            /* Rename the File */
            ftpRequest.RenameTo = newFileName;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }

        /* Create a New Directory on the FTP Server */
        public void createDirectory(string newDirectory) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }

        /* Get the Date/Time a File was Created */
        public string getFileCreatedDateTime(string fileName) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            var ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try {
                fileInfo = ftpReader.ReadToEnd();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Created Date Time */
            return fileInfo;
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* Get the Size of a File */
        public string getFileSize(string fileName) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            var ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try {
                while (ftpReader.Peek() != -1) fileInfo = ftpReader.ReadToEnd();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Size */
            return fileInfo;
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* List Directory Contents File/Folder Name Only */
        public string[] directoryListSimple(string directory) {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary  = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive  = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            var ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            try {
                while (ftpReader.Peek() != -1) directoryRaw += ftpReader.ReadLine() + "|";
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            try {
                string[] directoryList = directoryRaw.Split("|".ToCharArray());
                return directoryList;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Return an Empty string Array if an Exception Occurs */
            return new[] { "" };
        }

        /* List Directory Contents in Detail (Name, Size, Created, etc.) */
        public string[] directoryListDetailed(string directory) {
            try {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary  = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive  = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                var ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                try {
                    while (ftpReader.Peek() != -1) directoryRaw += ftpReader.ReadLine() + "|";
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }

                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                try {
                    string[] directoryList = directoryRaw.Split("|".ToCharArray());
                    return directoryList;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            /* Return an Empty string Array if an Exception Occurs */
            return new[] { "" };
        }

        public string[] GetDirectoryInformation(string directory) {
            string[] FullList = directoryListSimple(directory);
            var      files    = new List<string>();
            var      folders  = new Queue<string>();

            var request = (FtpWebRequest)WebRequest.Create(host + "/" + directory);
            request.Method      = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(user, pass);
            request.UsePassive  = true;
            request.UseBinary   = true;
            request.KeepAlive   = false;

            var      returnValue = new List<DirectoryItem>();
            string[] list        = null;

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream())) {
                list = reader.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (string line in list) {
                // Windows FTP Server Response Format 
                // Parse name
                string name = line;
                if (line.Trim().ToLower().StartsWith("d") || line.Contains(" <DIR> ")) {
                    ///é uma pasta não faz nada
                }
                else {
                    // Create directory info
                    foreach (string item in FullList) {
                        string[] l = item.Split('/');
                        if (name.Contains(l[l.Length - 1]) && item != "") {
                            files.Add(item);
                            break;
                        }
                    }
                }
            }

            return files.ToArray();
        }
    }
}