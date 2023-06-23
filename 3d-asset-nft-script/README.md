# 3D Asset NFTs

A simple script to demonstrate how to create 3D asset NFTs (or any NFTs that point to custom metadata) using the [SDK](https://portal.thirdweb.com/sdk).

## How to Use This Script

1. Create a copy of this template on your local machine:

```bash
npx thirdweb@latest create --template 3d-asset-nft-script
```

2. Setup your private key to sign transactions:

NOTE: in this repository, we use environment variables to store our private key.

We strongly recommend reading our [Securely Storing Private Keys](https://portal.thirdweb.com/sdk/set-up-the-sdk/securing-your-private-key) guide to learn more about how to securely store your private key.

Create a `.env` file in the root of your project and add your private key:

```bash
PRIVATE_KEY=xxx
```

3. In the [index.ts](./src/index.ts) file, update all of the variables at the top of the file to match your project.

4. Run the script to mint your NFT:

```bash
npx ts-node src/index.ts
```

## Questions?

Jump into our [Discord](https://discord.com/invite/thirdweb) to speak with us directly.
