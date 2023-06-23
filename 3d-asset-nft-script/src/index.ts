import { ChainOrRpc, ThirdwebSDK } from "@thirdweb-dev/sdk";
import "dotenv/config";

// === UPDATE THESE VALUES TO MATCH YOUR CONTRACT AND NETWORK ===
const NFT_COLLECTION_ADDRESS = "0xdCd1fcc7Fb8F78dd2b87C893FE0B3459a1705645"; // The address of the edition contract
const network: ChainOrRpc = "goerli"; // The network your contracts are deployed to
const PRIVATE_KEY = process.env.PRIVATE_KEY!; // Read the README for how to set this up in a .env file
const IPFS_URL = "https://ipfs-2.thirdwebcdn.com/ipfs/Qme9FN6Y4eRWvPpUbSnZPAzBad5KPUBNS5J9D3QuZ2KSWz"; // The URL of the asset bundle you uploaded to IPFS
// =========================================================== \\

(async () => {
  try {
    // Instantiate the SDK with our private key onto the network
    const sdk = ThirdwebSDK.fromPrivateKey(PRIVATE_KEY, network);
    const collection = await sdk.getContract(
      NFT_COLLECTION_ADDRESS,
      "nft-collection"
    );

    await collection.mint({
      name: "My 3D Cube ",
      description: "This NFT gets loaded in the Unity game at run time!",
      image: IPFS_URL,
    });

    console.log("🎉 Successfully minted NFT!");
  } catch (e) {
    console.error(e);
  }
})();
