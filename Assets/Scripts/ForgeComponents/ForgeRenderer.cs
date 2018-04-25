using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Platforge {

    [RequireComponent(typeof(Renderer))]
    public class ForgeRenderer : ForgeComponent {

        Renderer r;

        public Color Color {
            get {
                return r.material.GetColor("_Color");
            } set {
                r.material.SetColor("_Color",value);
            }
        }

        public Color EmissionColor {
            get {
                return r.material.GetColor("_EmissionColor");
            } set {
                r.material.SetColor("_EmissionColor", value);
            }
        }

        private void Awake() {
            r = GetComponent<Renderer>();
        }

        // Use this for initialization
        void Start() {
            DoStart();
        }

        // Update is called once per frame
        void Update() {

        }

        public override void DeserializeData(XmlElement data) {
            Color c;

            ColorUtility.TryParseHtmlString(data.GetAttribute("color"),out c);
            Color = c;

            ColorUtility.TryParseHtmlString(data.GetAttribute("emission-color"), out c);
            EmissionColor = c;
        }

        public override XmlElement SerializeData(XmlDocument doc) {
            XmlElement element = base.SerializeData(doc);

            element.SetAttribute("color", string.Empty, "#" + ColorUtility.ToHtmlStringRGBA(Color));
            element.SetAttribute("emission-color", string.Empty, "#" + ColorUtility.ToHtmlStringRGBA(EmissionColor));

            return element;
        }

        private void OnDestroy() {
            Destroy(r);
        }
    }
}
