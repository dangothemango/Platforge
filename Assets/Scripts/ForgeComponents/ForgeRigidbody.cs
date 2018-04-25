using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Platforge {

    [RequireComponent(typeof(Rigidbody))]
    public class ForgeRigidbody : ForgeComponent {

        Rigidbody rb;

        public float Mass {
            get {
                return rb.mass;
            }

            set {
                rb.mass = value;
            }
        }

        public float Drag {
            get {
                return rb.drag;
            }

            set {
                rb.drag = value;
            }
        }

        public float AngularDrag {
            get {
                return rb.angularDrag;
            }

            set {
                rb.angularDrag = value;
            }
        }

        public bool UseGravity {
            get {
                return rb.useGravity;
            }

            set {
                rb.useGravity = value;
            }
        }

        public RigidbodyConstraints Constraints {
            get {
                return rb.constraints;
            }

            set {
                rb.constraints = value;
            }
        }

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start() {
            DoStart();
        }

        public override void DeserializeData(XmlElement data) {
            Mass = float.Parse(data.GetAttribute("mass", string.Empty));
            Drag = float.Parse(data.GetAttribute("drag", string.Empty));
            AngularDrag = float.Parse(data.GetAttribute("angular-drag", string.Empty));
            UseGravity = bool.Parse(data.GetAttribute("use-gravity", string.Empty));
            Constraints = RigidbodyConstraintsParse(data.GetAttribute("constraints", string.Empty));
        }

        public override XmlElement SerializeData(XmlDocument doc) {
            XmlElement element = base.SerializeData(doc);

            element.SetAttribute("mass", string.Empty, Mass.ToString("G4"));
            element.SetAttribute("drag", string.Empty, Drag.ToString("G4"));
            element.SetAttribute("angular-drag", string.Empty, AngularDrag.ToString("G4"));
            element.SetAttribute("use-gravity", string.Empty, UseGravity.ToString());
            element.SetAttribute("constraints", string.Empty, Constraints.ToString());

            return element;
        }

        RigidbodyConstraints RigidbodyConstraintsParse(string rbc) {
            switch (rbc) {
                case "None":
                    return RigidbodyConstraints.None;
                case "FreezePosition":
                    return RigidbodyConstraints.FreezePosition;
                case "FreezeRotation":
                    return RigidbodyConstraints.FreezeRotation;
                case "FreezeAll":
                    return RigidbodyConstraints.FreezeAll;
                default:
                    return RigidbodyConstraints.None;
            }
        }

        private void OnDestroy() {
            Destroy(rb);
        }
    }

}
