namespace ProductInformationApp
{
    public partial class FlexPage : ContentPage
    {
        public record KeyValue(string Key, object Value);

        public IEnumerable<KeyValue> Items { get; set; } = new List<KeyValue>();

        public FlexPage()
        {
            InitializeComponent();
            Items = new List<KeyValue>() {
                { new KeyValue("Key1", "Value1") },
                { new KeyValue("Key2", "Value2") },
                { new KeyValue("Key3", "Value3") }

            };

            BindingContext = this;
        }
    }
}
