using System;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.Signer;
using Newtonsoft.Json;
using Thirdweb.Wallets;

namespace Thirdweb
{
    public class ThirdwebInterceptor : RequestInterceptor
    {
        private readonly IThirdwebWallet _thirdwebWallet;

        public ThirdwebInterceptor(IThirdwebWallet thirdwebWallet)
        {
            _thirdwebWallet = thirdwebWallet;
        }

        public override async Task<object> InterceptSendRequestAsync<T>(Func<RpcRequest, string, Task<T>> interceptedSendRequestAsync, RpcRequest request, string route = null)
        {
            UnityEngine.Debug.Log($"{request.Method} Request Intercepted: {JsonConvert.SerializeObject(request.RawParameters)}");

            if (request.Method == "eth_accounts")
            {
                var addy = await _thirdwebWallet.GetAddress();
                return new string[] { addy };
            }
            else if (request.Method == "personal_sign")
            {
                var signerWeb3 = await _thirdwebWallet.GetSignerWeb3();

                switch (_thirdwebWallet.GetProvider())
                {
                    case WalletProvider.LocalWallet:
                        var message = request.RawParameters[0].ToString();
                        return new EthereumMessageSigner().EncodeUTF8AndSign(message, new EthECKey(_thirdwebWallet.GetLocalAccount().PrivateKey));
                    case WalletProvider.SmartWallet:
                        return await signerWeb3.Client.SendRequestAsync<T>("personal_sign", null, request.RawParameters);
                    default:
                        break;
                }
            }
            else if (request.Method == "eth_signTypedData_v4")
            {
                var signerWeb3 = await _thirdwebWallet.GetSignerWeb3();

                switch (_thirdwebWallet.GetProvider())
                {
                    case WalletProvider.LocalWallet:
                        throw new Exception("Please use Wallet.SignTypedDataV4 instead when using Local Wallet as the signer.");
                    case WalletProvider.SmartWallet:
                        return await signerWeb3.Client.SendRequestAsync<T>("eth_signTypedData_v4", null, request.RawParameters);
                    default:
                        break;
                }
            }

            return await interceptedSendRequestAsync(request, route);
        }

        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<string, string, object[], Task<T>> interceptedSendRequestAsync,
            string method,
            string route = null,
            params object[] paramList
        )
        {
            UnityEngine.Debug.Log($"{method} Request Intercepted: {JsonConvert.SerializeObject(paramList)}");

            if (method == "eth_accounts")
            {
                var addy = await _thirdwebWallet.GetAddress();
                return new string[] { addy };
            }
            else if (method == "personal_sign")
            {
                var signerWeb3 = await _thirdwebWallet.GetSignerWeb3();

                switch (_thirdwebWallet.GetProvider())
                {
                    case WalletProvider.LocalWallet:
                        var message = paramList[0].ToString();
                        return new EthereumMessageSigner().EncodeUTF8AndSign(message, new EthECKey(_thirdwebWallet.GetLocalAccount().PrivateKey));
                    case WalletProvider.SmartWallet:
                        return await signerWeb3.Client.SendRequestAsync<T>("personal_sign", null, paramList);
                    default:
                        break;
                }
            }
            else if (method == "eth_signTypedData_v4")
            {
                var signerWeb3 = await _thirdwebWallet.GetSignerWeb3();

                switch (_thirdwebWallet.GetProvider())
                {
                    case WalletProvider.LocalWallet:
                        throw new Exception("Please use Wallet.SignTypedDataV4 instead when using Local Wallet as the signer.");
                    case WalletProvider.SmartWallet:
                        return await signerWeb3.Client.SendRequestAsync<T>("eth_signTypedData_v4", null, paramList);
                    default:
                        break;
                }
            }

            return await interceptedSendRequestAsync(method, route, paramList);
        }
    }
}
