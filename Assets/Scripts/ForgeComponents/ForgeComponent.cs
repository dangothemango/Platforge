﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Platforge {

    
    public class ForgeComponent : MonoBehaviour, IForgeableObject {

        static readonly System.Type[] typesArray =  {
            typeof(ForgeComponent),
            typeof(ForgeRigidbody),
            typeof(ForgeRenderer)
        };

        static Dictionary<string,System.Type> typesDict = new Dictionary<string, Type>();

        private static void PrepareTypesDict() {
            foreach (System.Type t in typesArray) {
                typesDict.Add(t.Name, t);
            }
        }

        public static Dictionary<string,System.Type> Types {
            get {
                if (typesDict.Count == 0) {
                    PrepareTypesDict();
                }
                return typesDict;
            }
        }

        private void Awake() {
            DoAwake();
        }

        protected virtual void DoAwake() {

        }

        private void Start() {
            DoStart();
        }

        protected virtual void DoStart() {
            if (!Types.ContainsKey(this.GetType().Name)) {
                throw new Exception(string.Format("Type {0} not included in ForgeComponent.types array" , this.GetType().Name));
            }
        }

        private void Update() {
            DoUpdate();
        }

        protected virtual void DoUpdate() {

        }

        public virtual void DeserializeData(XmlElement data) {
            throw new NotImplementedException();
        }

        public virtual XmlElement SerializeData(XmlDocument doc) {
            XmlElement element = doc.CreateElement(string.Empty, "Data", string.Empty);

            return element;
        }
        

    }
}
