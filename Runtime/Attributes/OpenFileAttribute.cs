using UnityEngine;

namespace scg.uitoolkit.runtime
{
    public class OpenFileAttribute : PropertyAttribute
    {
        public bool SelectDirectory { get; set; } = false;
        public bool RelativeToAssetDirectory { get; set; } = false;

        public OpenFileAttribute(){}

        public OpenFileAttribute(bool selectDirectory=false, bool relativeToAssetDirectory=false)
        {
            this.SelectDirectory = selectDirectory;
            this.RelativeToAssetDirectory = relativeToAssetDirectory;
        }

        
    }
}