using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace Platforge {

    public class ForgeManager : MonoBehaviour {

        [SerializeField]
        GameObject[] primitives;

        [SerializeField]
        Component[] components;

        Dictionary<string, GameObject> primitiveDict;

        string xmlFilename = "Assets/ScenesXML/Scene.xml";

        // Use this for initialization
        void Start() {

            primitiveDict = new Dictionary<string, GameObject>();
            for (int i = 0; i < primitives.Length; i++) {
                primitiveDict.Add(primitives[i].name, primitives[i]);
            }

            LoadScene();
        }

        void LoadScene() {
            //TODO remove this, probably dont want to delete everything everytime theo foremanager starts
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilename);


            foreach (XmlNode node in doc.ChildNodes) {
                if (node is XmlElement) {
                    XmlElement element = node as XmlElement;
                    switch (element.LocalName) {
                        case "Scene":
                            ParseObjects(transform, element);
                            break;
                    }
                }
            }
        }

        void ParseObjects(Transform parent, XmlElement element) {

            GameObject go;
            string objectType = element.GetAttribute("name");
            if (primitiveDict.ContainsKey(objectType)) {
                
                go = Instantiate(primitiveDict[objectType], parent);
                Utilities.DeserializeTransform(element["Transform"], go.transform);
            } else {
                go = this.gameObject;
            }

            foreach (XmlNode node in element["Children"]) {
                if (node is XmlElement) {
                    XmlElement e = node as XmlElement;
                    ParseObjects(go.transform, e);
                }
            }

            foreach (XmlNode node in element["Components"]) {
                if (node is XmlElement) {
                    XmlElement e = node as XmlElement;
                    string componentType = e.GetAttribute("type");
                    ForgeComponent fc = go.AddComponent(ForgeComponent.Types[componentType]) as ForgeComponent;
                    fc.DeserializeData(e["Data"]);
                }
            }
        }


        private void OnDestroy() {
            SaveAll();
        }

        private void SaveAll() {
            Debug.Log("Building XML...");

            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement scene = doc.CreateElement(string.Empty, "Scene", string.Empty);
            doc.AppendChild(scene);

            XmlElement children = doc.CreateElement(string.Empty, "Children", string.Empty);
            scene.AppendChild(children);

            XmlElement components = doc.CreateElement(string.Empty, "Components", string.Empty);
            scene.AppendChild(components);

            foreach (Transform t in transform) {
                XmlElement e = SaveObjectAndChildren(t, doc);
                children.AppendChild(e);
            }

            if (!Directory.Exists("Assets/ScenesXML")) {
                Directory.CreateDirectory("Assets/ScenesXML");
            }
            doc.Save(xmlFilename);

            Debug.Log("XML Built Successfully");
        }

        XmlElement SaveObjectAndChildren(Transform parent, XmlDocument doc) {
            XmlElement obj = doc.CreateElement(string.Empty, "Object", string.Empty);
            obj.SetAttribute("name", string.Empty, parent.gameObject.name.Replace("(Clone)", ""));
            obj.AppendChild(Utilities.SerializeTransform(parent, doc));

            XmlElement children = doc.CreateElement(string.Empty, "Children", string.Empty);
            obj.AppendChild(children);
            foreach (Transform t in parent) {
                XmlElement e = SaveObjectAndChildren(t, doc);
                children.AppendChild(e);
            }

            XmlElement components = doc.CreateElement(string.Empty, "Components", string.Empty);
            obj.AppendChild(components);
            foreach (ForgeComponent fc in parent.GetComponents<ForgeComponent>()) {
                XmlElement component = doc.CreateElement(string.Empty, "Component", string.Empty);
                component.SetAttribute("type",string.Empty, fc.GetType().Name);
                component.AppendChild(fc.SerializeData(doc));
                components.AppendChild(component);
            }

            return obj;
        }
    }
}
