using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Platforge {

    public static class Utilities {

        static readonly char[] parentheses = { '(', ')' };

        public static XmlElement SerializeTransform(Transform t, XmlDocument doc) {
            Quaternion q = t.localRotation;
            Vector4 rotation = new Vector4(q.x, q.y, q.z, q.w);
            Vector3 scale = t.localScale;
            Vector3 position = t.localPosition;
            XmlElement element = doc.CreateElement(string.Empty, "Transform", string.Empty);
            element.SetAttribute("rotation", string.Empty, rotation.ToString("G4"));
            element.SetAttribute("position", string.Empty, position.ToString("G4"));
            element.SetAttribute("scale", string.Empty, scale.ToString("G4"));
            return element;
        }

        public static void DeserializeTransform(XmlElement element, Transform t) {
            if (element.LocalName != "Transform") {
                throw new System.Exception("XmlElement is not of type Transform");
            }
            Vector3 position = Vec3FromString(element.GetAttribute("position", string.Empty));
            Vector3 scale = Vec3FromString(element.GetAttribute("scale", string.Empty));
            Vector4 rotation = Vec4FromString(element.GetAttribute("rotation", string.Empty));

            t.localPosition = position;
            t.localScale = scale;
            Quaternion q = new Quaternion(rotation.x,rotation.y,rotation.z,rotation.w);
            t.localRotation = q;
        }

        public static Vector3 Vec3FromString(string s) {
            s= s.Trim(parentheses);
            string[] vecValues = s.Split(',');
            return new Vector3(
                                float.Parse(vecValues[0].Trim()),
                                float.Parse(vecValues[1].Trim()),
                                float.Parse(vecValues[2].Trim())
                                );
        }

        public static Vector4 Vec4FromString(string s) {
            s=s.Trim(parentheses);
            string[] vecValues = s.Split(',');
            return new Vector4(
                                float.Parse(vecValues[0].Trim()),
                                float.Parse(vecValues[1].Trim()),
                                float.Parse(vecValues[2].Trim()),
                                float.Parse(vecValues[3].Trim())
                                );
        }

    }

}
