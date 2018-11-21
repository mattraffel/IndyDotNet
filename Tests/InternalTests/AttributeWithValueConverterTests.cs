using System;
using IndyDotNet.AnonCreds;
using IndyDotNet.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.InternalTests
{
    [TestClass]
    public class AttributeWithValueConverterTests
    {
        [TestMethod]
        public void ConvertsAttributeListToJsonSuccessfully()
        {
            AttributeValuesList list = new AttributeValuesList();
            list.Add(new AttributeWithValue()
            {
                Name = "name_field",
                CheckValue = "check_value_field",
                Value = "value_field"
            });

            string json = list.ToJson();
            string expectedJson = "{\"name_field\":[\"value_field\",\"check_value_field\"]}";

            Assert.AreEqual(expectedJson, json, $"AttributeValuesList output is {json}");
        }

        [TestMethod]
        public void ConvertsLargerAttributeListToJsonSuccessfully()
        {
            AttributeValuesList list = new AttributeValuesList();
            list.Add(new AttributeWithValue()
            {
                Name = "name_field_1",
                CheckValue = "check_value_field_1",
                Value = "value_field_1"
            });

            list.Add(new AttributeWithValue()
            {
                Name = "name_field_2",
                CheckValue = "check_value_field_2",
                Value = "value_field_2"
            });

            string json = list.ToJson();
            string expectedJson = "{\"name_field_1\":[\"value_field_1\",\"check_value_field_1\"],\"name_field_2\":[\"value_field_2\",\"check_value_field_2\"]}";

            Assert.AreEqual(expectedJson, json, $"AttributeValuesList output is {json}");
        }
    }
}
