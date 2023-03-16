using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace DesignPatterns.Creational.Factory
{
    //a factory can keep track of every object that creates.
    //you would want to use a weak reference so as not to extend the lifetime of a constructed object, because otherwise the 
    //objects will live for as long as the factory lives 
    //we can also replace these objects if have a track of them
    public interface ITheme
    {
        string TextColor => "black";
        string BgrColor => "White";
    }

    class DarkTheme : ITheme
    {
        public string TextColor => "black";
        public string BgrColor => "White";
    }

    class LightTheme : ITheme
    {
        public string TextColor => "White";
        public string BgrColor => "Dark Grey";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new List<WeakReference<ITheme>>();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme;
            if (dark) { theme = new DarkTheme(); } else { theme = new LightTheme(); };
            themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var refernce in themes)
                {
                    if (refernce.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light").AppendLine("theme");
                    }
                }
                return sb.ToString();
            }
        }
    }


    public class Ref<T> where T : class
    {
        public T value;
        public Ref(T value)
        {
            this.value = value;
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> themes = new List<WeakReference<Ref<ITheme>>>();
        public ITheme CreateThemeImpl(bool dark) {
            if (dark) { return new DarkTheme(); } else { return new LightTheme(); };
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var obj = new Ref<ITheme>(CreateThemeImpl(dark));
            themes.Add(new(obj));
            return obj;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach(var wr in themes)
            {
                if(wr.TryGetTarget(out var theme)) { 
                    theme.value = CreateThemeImpl(dark);
                }
            }
        }
    }


    internal class ObjectTracking
    {
        static void Main(string[] args)
        {
            var factory = new TrackingThemeFactory();
            var theme = factory.CreateTheme(true);
            var theme2 = factory.CreateTheme(false);
            Console.WriteLine(factory.Info);
            //dark theme
            //light theme

            //replacement
            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.value.BgrColor); //dark gray
            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.value.BgrColor); //white
        }
    }
}
