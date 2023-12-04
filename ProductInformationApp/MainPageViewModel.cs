using ProductInformationApp.Services;
using Prins;
using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;

namespace ProductInformationApp
{
    partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Overview))]
        [NotifyPropertyChangedFor(nameof(CommonInformation))]
        [NotifyPropertyChangedFor(nameof(Hints))]
        [NotifyPropertyChangedFor(nameof(TradingUnit))]
        [NotifyPropertyChangedFor(nameof(UnitPack))]
        [NotifyPropertyChangedFor(nameof(Palette))]
        [NotifyPropertyChangedFor(nameof(Ingredients))]
        [NotifyPropertyChangedFor(nameof(NutrientAnnotation))]
        [NotifyPropertyChangedFor(nameof(Nutrients))]
        [NotifyPropertyChangedFor(nameof(Additives))]
        [NotifyPropertyChangedFor(nameof(Alergens))]
        [NotifyPropertyChangedFor(nameof(Chunks))]
        Product product;

        public record KeyValue(string Key, object Value);

        public record TextCell(string Title, IEnumerable<object> Body);

        public record KeyValueUnit(string Key, object Value, string Unit);

        public record ProductInfoChunk(string Header, IEnumerable<object> Data);



        public ImmutableList<KeyValue> Overview 
        { 
            get => Product.GetOverview().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList(); 
        }

        public ImmutableList<KeyValue> CommonInformation
        { 
            get => Product.GetCommonInformation().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList(); 
        }



        public ImmutableList<TextCell> Hints
        { 
            get => Product.GetHints()
                        .Select(o => {
                            var key = o.Item1;
                            var value = o.Item2;
                            if (value is not string && value is IEnumerable items) {
                                return new TextCell(key, items.Cast<object>());
                            }
                            return new TextCell(key, new object[] { value }); 
                        })
                        .ToImmutableList(); 
        }



        public ImmutableList<KeyValue> TradingUnit
        { 
            get => Product.GetTradingUnit().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> UnitPack
        { 
            get => Product.GetUnitPack().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Palette
        { 
            get => Product.GetPaletteInfo().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }



        public string Ingredients { get => Product?.Ingredients?.Value ?? "-"; }

        public string NutrientAnnotation { get => Product?.NutrientAnnotation?.Value ?? "-"; }

        public ImmutableList<KeyValueUnit> Nutrients 
        { 
            get => Product.GetNutrients().Select(o => new KeyValueUnit(o.Item1, o.Item2, o.Item3)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Additives 
        { 
            get => Product.GetAdditives().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Alergens 
        { 
            get => Product.GetAlergens().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public IEnumerable<object> Chunks { get => GetChunks(); }

        public IEnumerable<object> GetChunks()
        {
            yield return new ProductInfoChunk(nameof(Overview), Overview);
            yield return new ProductInfoChunk(nameof(CommonInformation), CommonInformation);
            yield return new ProductInfoChunk(nameof(Hints), Hints);

            yield return new ProductInfoChunk(nameof(TradingUnit), TradingUnit);
            yield return new ProductInfoChunk(nameof(UnitPack), UnitPack);
            yield return new ProductInfoChunk(nameof(Palette), Palette);

            yield return new ProductInfoChunk(nameof(Ingredients), 
                new string[] { Ingredients });
            yield return new ProductInfoChunk(nameof(NutrientAnnotation), 
                new string[] { NutrientAnnotation });
            yield return new ProductInfoChunk(nameof(Nutrients), Nutrients);
            yield return new ProductInfoChunk(nameof(Additives), Additives);
            yield return new ProductInfoChunk(nameof(Alergens), Alergens);
        }

        public MainPageViewModel(Product product)
        {
            Product = product;
        }
    }
}