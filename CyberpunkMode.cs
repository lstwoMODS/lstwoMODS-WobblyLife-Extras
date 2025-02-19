using lstwoMODS_Core;
using lstwoMODS_Core.Hacks;
using lstwoMODS_Core.UI.TabMenus;
using UnityEngine;

namespace NotAzzamods.Hacks
{
    public class CyberpunkMode : BaseHack
    {
        public override string Name => "Cyberpunk Mode";

        public override string Description => "";

        public override HacksTab HacksTab => lstwoMODS_WobblyLife.Plugin.ExtraHacksTab;

        public override void ConstructUI(GameObject root)
        {
            var ui = new HacksUIHelper(root);

            ui.AddSpacer(6);

            ui.CreateLBDuo("Enable Cyberpunk Mode (Cannot be disabled)", "name", () =>
            {
                Time.fixedDeltaTime = 0.05f;
                foreach (var rb in UnityEngine.Object.FindObjectsOfType<Rigidbody>())
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                }
            }, "Apply", "lstwo.CyberpunkMode.Enable");

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
