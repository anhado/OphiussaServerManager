namespace OphiussaServerManager.Common {
    public interface INullableValue {
        bool HasValue { get; }

        INullableValue Clone();

        void SetValue(object value);
    }
}