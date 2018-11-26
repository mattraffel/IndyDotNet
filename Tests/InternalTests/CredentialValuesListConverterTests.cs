using System;
using IndyDotNet.AnonCreds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.InternalTests
{
    [TestClass]
    public class CredentialValuesListConverterTests
    {
        [TestMethod]
        public void CredentialValueListOfOneItemConvertedSuccessfully()
        {
            /*
                {  
                      "height":{  
                         "raw":"175",
                         "encoded":"175"
                      },
                      "sex":{  
                         "raw":"male",
                         "encoded":"5944657099558967239210949258394887428692050081607692519917050011144233115103"
                      },
                      "age":{  
                         "raw":"27",
                         "encoded":"27"
                      },
                      "name":{  
                         "raw":"Alex",
                         "encoded":"99262857098057710338306967609588410025648622308394250666849665532448612202874"
                      }
                   }
             */

            string json = "{\"height\":{\"raw\":\"175\",\"encoded\":\"175\"}}";
            CredentialValuesList list = JsonConvert.DeserializeObject<CredentialValuesList>(json);

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);

            IssuerCredentialValue item = list[0];

            Assert.IsNotNull(item);
            Assert.AreEqual("height", item.Name);
            Assert.AreEqual("175", item.Raw);
            Assert.AreEqual("175", item.Encoded);
        }

        [TestMethod]
        public void CredentialValueListOfTwoItemsConvertedSuccessfully()
        {
            /*
                {  
                      "height":{  
                         "raw":"175",
                         "encoded":"175"
                      },
                      "sex":{  
                         "raw":"male",
                         "encoded":"5944657099558967239210949258394887428692050081607692519917050011144233115103"
                      },
                      "age":{  
                         "raw":"27",
                         "encoded":"27"
                      },
                      "name":{  
                         "raw":"Alex",
                         "encoded":"99262857098057710338306967609588410025648622308394250666849665532448612202874"
                      }
                   }
             */

            string json = "{\"height\":{\"raw\":\"175\",\"encoded\":\"175\"},\"sex\":{\"raw\":\"male\",\"encoded\":\"5944657099558967239210949258394887428692050081607692519917050011144233115103\"}}";

            CredentialValuesList list = JsonConvert.DeserializeObject<CredentialValuesList>(json);

            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            IssuerCredentialValue item = list[0];

            Assert.IsNotNull(item);
            Assert.AreEqual("height", item.Name);
            Assert.AreEqual("175", item.Raw);
            Assert.AreEqual("175", item.Encoded);

            item = list[1];

            Assert.IsNotNull(item);
            Assert.AreEqual("sex", item.Name);
            Assert.AreEqual("male", item.Raw);
            Assert.AreEqual("5944657099558967239210949258394887428692050081607692519917050011144233115103", item.Encoded);

        }
    }
}
