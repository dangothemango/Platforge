using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platforge {

    public class ForgeMonoBehaviour : ForgeComponent {

        public List<ForgeAssembly.Command> awakeList;
        public List<ForgeAssembly.Command> startList;
        public List<ForgeAssembly.Command> updateList;

        private void Awake() {
            DoAwake();
        }

        // Use this for initialization
        void Start() {
            DoStart();
        }

        // Update is called once per frame
        void Update() {
            DoUpdate();
        }

        protected override void DoAwake() {
            base.DoAwake();
            ForgeAssembly.RunCommandList(awakeList);
        }

        protected override void DoStart() {
            base.DoStart();
            ForgeAssembly.RunCommandList(startList);
        }

        protected override void DoUpdate() {
            base.DoUpdate();
            ForgeAssembly.RunCommandList(updateList);
        }
    }
}
