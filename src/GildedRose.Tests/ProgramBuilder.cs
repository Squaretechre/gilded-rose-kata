using GildedRose.Console;

namespace GildedRose.Tests
{
    internal class ProgramBuilder
    {
        private readonly Program _program;

        public ProgramBuilder()
        {
            _program = Program.CreateProgram();
        }

        public ProgramBuilder WithUpdatedQuality(int times)
        {
            for (var i = 0; i < times; i++)
            {
                _program.UpdateQuality();
            }
            return this;
        }

        public Program Build() => _program;
    }
}