using UnityEngine;
using lstwoMODS_Core;
using lstwoMODS_Core.UI.TabMenus;
using lstwoMODS_Core.Hacks;
using lstwoMODS_WobblyLife;

namespace NotAzzamods.Hacks
{
    public class AccuratePhysicsMode : BaseHack
    {
        public override string Name => "\"stop whining about physics going wrong\" mode";

        public override string Description => "";

        public override HacksTab HacksTab => lstwoMODS_WobblyLife.Plugin.ExtraHacksTab;

        public override void ConstructUI(GameObject root)
        {
            var ui = new HacksUIHelper(root);

            ui.AddSpacer(6);

            ui.CreateLBDuo("Enable (Cannot be disabled)", "name", () =>
            {
                foreach (var rb in Object.FindObjectsOfType<Rigidbody>())
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
            }, "Apply", "lstwo.AccuratePhysicsMode.Enable");

            ui.AddSpacer(6);
        }

        public override void RefreshUI()
        {
        }

        public override void Update()
        {
        }
    }
}
