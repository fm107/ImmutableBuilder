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

    public class SingleDestinationBuilder: BuilderBase<SingleDestinationBuilder>
    {
        private string _destination;

        public SingleDestinationBuilder WithDestination(string destination)
        {
            var instance = CreateInstance();
            instance._destination = destination;

            return instance;
        }

        protected override SingleDestinationBuilder CreateInstance()
        {
            return new SingleDestinationBuilder();
        }

        protected override IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            var baseEntries = base.EnumerateEntries();
            foreach (var baseEntry in baseEntries)
            {
                yield return baseEntry;
            }

            yield return KeyValuePair.Create("Destination", _destination);
        }
    }

    public class MultipleDestinationBuilder: BuilderBase<MultipleDestinationBuilder>
    {
        private IReadOnlyCollection<string> _destinations;

        public MultipleDestinationBuilder WithDestinations(params string[] destinations)
        {
            var instance = CreateInstance();
            instance._destinations = destinations;

            return instance;
        }

        protected override MultipleDestinationBuilder CreateInstance()
        {
            return new MultipleDestinationBuilder();
        }

        protected override IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            var baseEntries = base.EnumerateEntries();
            foreach (var baseEntry in baseEntries)
            {
                yield return baseEntry;
            }

            yield return KeyValuePair.Create("Destinations", string.Join(";", _destinations));
        }
    }

    public abstract class BuilderBase<T> where T : BuilderBase<T>
    {
        private string _source;

        public T WithSource(string source)
        {
            var instance = CreateInstance();
            instance._source = source;

            return instance;
        }

        protected abstract T CreateInstance();

        protected virtual IEnumerable<KeyValuePair<string, string>> EnumerateEntries()
        {
            yield return KeyValuePair.Create("Source", _source);
        }

        public IReadOnlyDictionary<string, string> Build()
        {
            var entries = EnumerateEntries();
            return new Dictionary<string, string>(entries);
        }
    }
 }
