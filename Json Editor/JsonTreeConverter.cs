using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Json_Editor
{
    internal class JsonTreeConverter
    {
        private TreeNode result = new TreeNode();
        public TreeNode Result { get => result; }

        public JsonTreeConverter(string json)
        {
            JToken jToken = JToken.Parse(json);
            result = ConvertToTreeNode("JSON root", jToken);
        }

        private TreeNode ConvertToTreeNode(string nodeText, JToken jToken)
        {
            TreeNode root = new TreeNode(nodeText);

            switch (jToken)
            {
                case JObject jObject:
                    foreach (JProperty jProperty in jObject.Properties())
                    {
                        TreeNode child = ConvertToTreeNode(jProperty.Name, jProperty.Value);
                        root.Nodes.Add(child);
                    }
                    return root;

                case JArray jArray:
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        TreeNode child = ConvertToTreeNode($"[{i}]", jArray[i]);
                        root.Nodes.Add(child);
                    }
                    return root;

                case JValue jValue:
                    TreeNode node = new TreeNode();

                    if (jValue.Value is null) node = new TreeNode("");
                    else node = new TreeNode(jValue.Value.ToString());

                    root.Nodes.Add(node);
                    return root;

                default:
                    return root;
            }
        }
    }
}
