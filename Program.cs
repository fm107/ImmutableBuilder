using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static IReadOnlyDictionary<string, string> BuildSingleDestination()
        {
            var singleDestinationBuilder = new SingleDestinationBuilder();

            return singleDestinationBuilder
                .WithSource("source")
                .WithDestination("destination")
                .Build();
        }

        static IReadOnlyDictionary<string, string> BuildMultipleDestination()
        {
            var multipleDestinationBuilder = new MultipleDestinationBuilder();

            return multipleDestinationBuilder
                .WithDestinations("destination 1", "destination 2")
                .WithSource("source")
                .Build();
        }
    }

    public class SingleDestinationBuilder: BuilderBase
    {
        private string _destination;

        public SingleDestinationBuilder WithDestination(string destination)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            var baseEntries = base.EnumerateEntries();
            foreach (var baseEntry in baseEntries)
            {
                yield return baseEntry;
            }

            yield return KeyValuePair.Create<string, string>("Destination", _destination);
        }
    }

    public class MultipleDestinationBuilder: BuilderBase
    {
        private IReadOnlyCollection<string> _destinations;

        public MultipleDestinationBuilder WithDestinations(params string[] destinations)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            var baseEntries = base.EnumerateEntries();
            foreach (var baseEntry in baseEntries)
            {
                yield return baseEntry;
            }

            yield return KeyValuePair.Create<string, string>("Destinations", string.Join(";", _destinations));
        }
    }

    public abstract class BuilderBase
    {
        private string _source;

        public BuilderBase WithSource(string source)
        {
            throw new NotImplementedException();
        }

        protected virtual IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            yield return KeyValuePair.Create<string, string>("Source", _source);
        }

        public IReadOnlyDictionary<string, string> Build()
        {
            var entries = EnumerateEntries();
            return new Dictionary<string, string>(entries);
        }
    }
}