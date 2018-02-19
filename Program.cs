using System;
using System.Collections.Generic;

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
        private string _singleDestination;

        public SingleDestinationBuilder WithDestination(string destination)
        {
            throw new NotImplementedException();
        }
    }

    public class MultipleDestinationBuilder: BuilderBase
    {
        private IReadOnlyCollection<string> _destinations;

        public MultipleDestinationBuilder WithDestinations(params string[] destinations)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BuilderBase
    {
        private string _source;

        public BuilderBase WithSource(string source)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<string, string> Build()
        {
            throw new NotImplementedException();
        }
    }
}