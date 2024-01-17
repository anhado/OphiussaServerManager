using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OphiussaServerManager.Common.Helpers.Ini;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
     public class EngramAutoUnlockList : AggregateIniValueList<EngramAutoUnlock>
 {
     public EngramAutoUnlockList(string aggregateValueName)
         : base(aggregateValueName, null)
     {
     }

     public override void FromIniValues(IEnumerable<string> iniValues)
     {
         var items = iniValues?.Select(AggregateIniValue.FromINIValue<EngramAutoUnlock>);

         Clear();

         AddRange(items.Where(i => !this.Any(e => e.IsEquivalent(i))));

         foreach (var item in items.Where(i => this.Any(e => e.IsEquivalent(i))))
         {
             var e = this.FirstOrDefault(r => r.IsEquivalent(item));
             e.LevelToAutoUnlock = item.LevelToAutoUnlock;
         }

         IsEnabled = (Count != 0);

         Sort(AggregateIniValue.SortKeySelector);
     }

     public override IEnumerable<string> ToIniValues()
     {
         if (string.IsNullOrWhiteSpace(IniCollectionKey))
             return this.Where(d => d.ShouldSave()).Select(d => d.ToINIValue());

         return this.Where(d => d.ShouldSave()).Select(d => $"{this.IniCollectionKey}={d.ToINIValue()}");
     }
 }

 public class EngramAutoUnlock : AggregateIniValue
 { 
     [AggregateIniValueEntry] public string EngramClassName { get; set; }

     [AggregateIniValueEntry]
     public int LevelToAutoUnlock { get; set; }

     public static EngramAutoUnlock FromINIValue(string iniValue)
     {
         var engramAutoUnlock = new EngramAutoUnlock();
         engramAutoUnlock.InitializeFromINIValue(iniValue);
         return engramAutoUnlock;
     }

     public override string GetSortKey()
     {
         return null;
     }

     public override bool IsEquivalent(AggregateIniValue other)
     {
         return String.Equals(this.EngramClassName, ((EngramAutoUnlock)other).EngramClassName, StringComparison.OrdinalIgnoreCase);
     }

     public override string ToINIValue()
     {
         return base.ToINIValue();
     }
 }
}