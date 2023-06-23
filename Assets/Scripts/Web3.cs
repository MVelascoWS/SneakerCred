using System.Collections;
using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.Networking;

public class Web3 : MonoBehaviour
{
    private ThirdwebSDK sdk;
    private string assetBundleUrl;

    async void Start()
    {
        sdk = new ThirdwebSDK("goerli");
        await LoadNft();
        StartCoroutine(SpawnNft());
    }
    async Task<string> LoadNft()
    {
        var contract = 
            sdk.GetContract("0xdCd1fcc7Fb8F78dd2b87C893FE0B3459a1705645");
        var nft = await contract.GetNft("0");
        assetBundleUrl = nft.nft.metadata.image;
        return assetBundleUrl;
    }

    IEnumerator SpawnNft()
    {
        //Define the prefab to load
        string assetName = "Cube";

        //Request the asset bundle from the IPFS URL
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleUrl);
        yield return www.SendWebRequest();

        //Something failed with the request
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Network error");
            Debug.Log(www.error);
        }

        //Successfully downloaded the asset bundle, instantiate the prefab
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            GameObject prefab = bundle.LoadAsset<GameObject>(assetName);
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}