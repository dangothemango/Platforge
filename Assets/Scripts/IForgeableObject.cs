using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Platforge {

    public interface IForgeableObject {

        XmlElement SerializeData(XmlDocument doc);
        void DeserializeData(XmlElement data);

    }
}
