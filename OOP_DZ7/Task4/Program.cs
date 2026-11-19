using System;
using System.Text;

namespace Task4
{
    public abstract class ChristmasTree
    {
        public abstract string GetDescription();
        public abstract void Shine();
    }

    public class SimpleTree : ChristmasTree
    {
        public override string GetDescription() => "Звичайна ялинка";

        public override void Shine()
        {
            Console.WriteLine("Стан: ялинка не світиться.");
        }
    }

    public abstract class TreeDecorator : ChristmasTree
    {
        protected ChristmasTree _tree;

        protected TreeDecorator(ChristmasTree tree)
        {
            _tree = tree;
        }

        public override string GetDescription() => _tree.GetDescription();
        public override void Shine() => _tree.Shine();
    }

    public class Toys : TreeDecorator
    {
        public Toys(ChristmasTree tree) : base(tree) { }

        public override string GetDescription() => $"{_tree.GetDescription()} + іграшки";
    }

    public class Garland : TreeDecorator
    {
        public Garland(ChristmasTree tree) : base(tree) { }

        public override string GetDescription() => $"{_tree.GetDescription()} + гірлянда";

        public override void Shine()
        {
            Console.WriteLine("Стан: ялинка яскраво світиться!");
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Завдання 4: Декоратор (Decorator) ===\n");

            ChristmasTree simpleTree = new SimpleTree();
            Console.WriteLine(simpleTree.GetDescription());
            simpleTree.Shine();
            Console.WriteLine();

            ChristmasTree treeWithToys = new Toys(new SimpleTree());
            Console.WriteLine(treeWithToys.GetDescription());
            treeWithToys.Shine();
            Console.WriteLine();

            ChristmasTree treeWithGarland = new Garland(new SimpleTree());
            Console.WriteLine(treeWithGarland.GetDescription());
            treeWithGarland.Shine();
            Console.WriteLine();

            ChristmasTree decoratedTree = new Garland(new Toys(new SimpleTree()));
            Console.WriteLine(decoratedTree.GetDescription());
            decoratedTree.Shine();
        }
    }
}
