using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Platforge {

    [RequireComponent(typeof(Rigidbody))]
    public class ForgeRigidbody : ForgeComponent {

        Rigidbody rb;

        [Header("Config")]
        [SerializeField] float mass = 1f;
        [SerializeField] float drag = 0;
        [SerializeField] float angularDrag = 0.05f;
        [SerializeField] bool useGravity = true;
        [SerializeField] RigidbodyConstraints constraints = RigidbodyConstraints.None;

        public float Mass {
            get {
                return mass;
            }

            set {
                rb.mass = value;
                mass = value;
            }
        }

        public float Drag {
            get {
                return drag;
            }

            set {
                rb.drag = value;
                drag = value;
            }
        }

        public float AngularDrag {
            get {
                return angularDrag;
            }

            set {
                rb.angularDrag = value;
                angularDrag = value;
            }
        }

        public bool UseGravity {
            get {
                return useGravity;
            }

            set {
                rb.useGravity = value;
                useGravity = value;
            }
        }

        public RigidbodyConstraints Constraints {
            get {
                return constraints;
            }

            set {
                rb.constraints = constraints;
                constraints = value;
            }
        }

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start() {
            DoStart();
        }

        // Update is called once per frame
        void Update() {
            
        }

        public override void DeserializeData(XmlElement data) {
            Mass = float.Parse(data.GetAttribute("mass", string.Empty));
            Drag = float.Parse(data.GetAttribute("drag", string.Empty));
            AngularDrag = float.Parse(data.GetAttribute("angular-drag", string.Empty));
            UseGravity = bool.Parse(data.GetAttribute("use-gravity", string.Empty));
            constraints = RigidbodyConstraintsParse(data.GetAttribute("constraints", string.Empty));
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
            return RigidbodyConstraints.None;
        }
    }

}
