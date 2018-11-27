using System;
using IndyDotNet.BlobStorage;
using IndyDotNet.Did;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    internal class IssuerAnonCreds : IIssuerAnonCreds
    {
        private IWallet _wallet;

        internal IssuerAnonCreds(IWallet wallet)
        {
            _wallet = wallet;
        }

        public IssuerCredentialDefinition CreateStoreCredentialDef(IDid issuerDid, CredentialDefinitionSchema definition)
        {
            string schemaJson = definition.ToJson();
            string tag = definition.Tag;
            string configJson = definition.Config.ToJson();
            string signatureType = definition.SignatureType;

            //Logger.Info($"     schemaJson = {schemaJson}");
            //Logger.Info($"     configJson = {configJson}");
            //Logger.Info($"     tag = {tag}");
            //Logger.Info($"     signatureType = {signatureType}");

            IssuerCreateAndStoreCredentialDefResult result = AnonCredsAsync.IssuerCreateAndStoreCredentialDefAsync(_wallet, issuerDid, schemaJson, tag, signatureType, configJson).Result;

            return JsonConvert.DeserializeObject<IssuerCredentialDefinition>(result.CredDefJson);
        }

        public IssuerCredentialOffer CreateCredentialOffer(string credentialId)
        {
            string result = AnonCredsAsync.IssuerCreateCredentialOfferAsync(_wallet, credentialId).Result;

            return JsonConvert.DeserializeObject<IssuerCredentialOffer>(result);
        }

        public IssuerCredential CreateCredential(IssuerCredentialOffer claimOffer, ProverCredentialRequest request, AttributeValuesList attributeValues, string revcationId = null, IBlobStorageReader reader = null)
        {

            string claimOfferJson = claimOffer.ToJson();
            string credentialRequestJson = request.ToJson();
            string attributeValuesJson = attributeValues.ToJson();

            //Logger.Info($"\n     claimOfferJson = {claimOfferJson}");
            //Logger.Info($"\n     credentialRequestJson = {credentialRequestJson}");
            //Logger.Info($"\n     attributeValuesJson = {attributeValuesJson}");

            IssuerCreateCredentialResult credentialResult = AnonCredsAsync.IssuerCreateCredentialAsync(_wallet, claimOfferJson, credentialRequestJson, attributeValuesJson, revcationId, reader).Result;

            /* here's what comes back.
                {  
                   "schema_id":"id",
                   "cred_def_id":"Eo4fDS7JFYidRmCTYpxM8D:3:CL:1:TAG",
                   "rev_reg_id":null,
                   "values":{  
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
                   },
                   "signature":{  
                      "p_credential":{  
                         "m_2":"94141249536823555791556958352454414068832959837090304741968641158986385926676",
                         "a":"8323332288664252783427676821045457475612521784608814011541341923485587617725984777235181184890142629699108349760164383728212528853028501250918088262532060211844075264206669140313009693546189699251574685560213711517469903818703879074476239039078328620548517525898491838522877250431561078064751213053410590410101165438102222417530659061266835969167075535437902735033653056560765994534497100821935622244615239416863701177712259395727362404400435334042457516385116244472976813842575355024925419592558950377852585171254701784306916491778197264072325586166244416062710740654823911714771843967643357713104583763825844898603",
                         "e":"259344723055062059907025491480697571938277889515152306249728583105665800713306759149981690559193987143012367913206299323899696942213235956742930060377963820203874793638160040955661",
                         "v":"7116791841216643605806132922471918810530603205786140082127764783074742665808976581706290333160205346320350030185475272873444992655573813271903498274011266552422799355150146028910550269789156481465993272118732907571755105523497865615124405921516267663927425203942212450068377170838210496716497180444061091115871500878332700682503775305328976077032880441319647645266911962057541572276963031632084026970709990182627506528636976756150219524414872482370127261485905272143924636368905355144297418919251181316158735155809349770598838182048801142221550337446605104694255022037751989156123955485144312728746052576135240009582908593503850082616242661870092365307275549192424180347189490756108891521166863564342684115685835430770088452510084201376851238193804825429138278441268162228679560852216513635610718052218265015819730207633"
                      },
                      "r_credential":null
                   },
                   "signature_correctness_proof":{  
                      "se":"23517870644417168860936261855438589759353852590514979104397219907457258591177640505267679875002557681226621144357025639497346325770337643878487434876918481104566146211961273713122104201084305014358893221548189880301875133901670782733835206189071592927729376377681150774201104957206749648815536075161399954687422481722324731447954411973815350341650706632300090301180877699820357354765338496900150492757655845500340545065384336816379076413150453608294683709404586326701685229371750634388046970171288371882157492599450010625859267329399571040938050507851344981041236819163797087069796101746522362854436506022481968440685",
                      "c":"8496172404567780250403063769714240044570433755835923246846940171384309456027"
                   },
                   "rev_reg":null,
                   "witness":null
                }
             */


            //Logger.Info($"\n ------- CreateCredential ----------- ");
            //Logger.Info($"\n     credentialJson = {credentialResult.CredentialJson}");
            //Logger.Info($"\n ------------------------------------ ");

            return JsonConvert.DeserializeObject<IssuerCredential>(credentialResult.CredentialJson);
        }
    }
}
