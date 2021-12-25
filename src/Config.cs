using System;

namespace _7d2dFovChanger
{
    class Config
    {
        public static int weapon_fov = 60;

        internal static bool Load(string path)
        {
            Log.Out($"[FovChanger] Loading configuration {path}");

            var doc = new System.Xml.XmlDocument();
            try
            {
                doc.Load(path);

                var root = doc.DocumentElement;
                var settingsNode = root.SelectSingleNode("Settings");
                for (int i = 0; i < settingsNode.ChildNodes.Count; i++)
                {
                    var child = settingsNode.ChildNodes[i];
                    if (child.Name == "weapon_fov")
                    {
                        weapon_fov = int.Parse(child.Attributes.GetNamedItem("value").Value);
                        Log.Out($"[FovChanger] weapon_fov: {weapon_fov}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[FovChanger] Failed to read Config XML: {ex.Message}");
                Log.Exception(ex);

                return false;
            }

            return true;
        }
    }
}
