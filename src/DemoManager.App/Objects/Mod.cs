namespace DemoManager.App.Objects
{
    public class Mod
    {
        public Mod(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; }
        public string Path { get; }

        #region Overrides of Object

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}