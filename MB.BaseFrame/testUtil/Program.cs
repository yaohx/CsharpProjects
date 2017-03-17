using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testUtil
{
    class Program
    {
        static void Main(string[] args) {
            string val = "o;[1[5U_NE`P";
            string val2 = "bjmSL69kNb91/oXX7DKHUjnSrpDwsxDaij3zuX3IpBqHIPIEXFwa9I0F+Oa2hmwJF+A3IR8r/pGNoGvVWGJsBfiZG9MmTQVgqvuDwn7vyKReFz82K/LCUH+7XU7dF4hA7pvGvDqQXW5Xk0OJqdsqobMyVKZZnGWfp3/1c0RRFTSvnv8gid/ozIxqNE/9i7gnibxm8cX0nWE15KU4QiniI2jYOkawT8qhno7BdaoWaSQI00CQVI0m0ywOL6F37S/7QS3Ij4Qdlf+3hxr2hjWooSE5SKheIpzVI3be2NESD+PH/48gOMHCr/R9pP2IXCldSmkXp1PriCcMhFbHhv3rxnRXiO09aavVWNg89HgbK/PmIfAcW+jiK2sY4N5PXsIi6XyLch3lwtmYSsPklq9Kbn1pMgFsbhBP6TiQZrjSJJmhLAQOcyKsSP3nVX3ccBWNApYlgP5/dRgrbP4qZgZ3AL4MU8hTF6MLUYBna77NvfpU0zwLCs6Vi0mokMycXRA87gxdy8X+g/apsYqNSO7yVk2GrU/oTnQDiV7wYHrW4OxVDzB6ujp4yVX2QX7RafmxFyC8sqA5sJzSixgvMoQKHN8PJdqbOAkrQoilD8zS4+W1OxILZH0IzJW5VmljATQhY4yurCUF3XaHFCoWtrnoXhoyGEB7pNPI";

            string val3 = "bjmSL69kNb91/oXX7DKHUjnSrpDwsxDaij3zuX3IpBqHIPIEXFwa9I0F+Oa2hmwJS+vo6QefpyP/PJ/rV2ixjhcJ3g5xTnegeyFSbxS1wCF/y9jVvCP/de+mtgAI2+csWMN0xhFt0C6ZeHMsCXKJs6arqAkhVLRBRoInX/BqMrDpSw8wJfIB65i8Y4Dp7k+ThmRj7odZJJASsk7b4vyYcLGT55yfxXWMq61GyB1ju3Cf9rpd8cVPs43zo9c5ET8RWsdaDcBikrgDRba7IwXWQcPhkWAcXQ2Hca08II1ZJ+4k9tWrIdqv7BE5SS7gR9JHhMX5oHOjSX+rhOefgKhIhiUX6YzhfaUvMr6UlWCer9JC3a4mqwDH0ISgpnL9EXp13JwwqGCyou1SypSohnItG69oZGm1aJynB6r8FcOIfc9NaNebaITRJQgsEcub1JUZu46sN0kP2dicsEOd9y///fh3KXgmHrC+jKqFq4YMk37ygIAmT3IJY5ikOkzGjD44ZmERiFJgfBTwT+2pMs7xKgFgl2/BIHs7Ws6uhlBRwO9s50pEsO/g2D1PHnUiKKD6C4yhHISjG9ewTxO7Pi6JCke/0WVDVmkh7Cz3hMckNdtG6D2sqw8Vr5eoXPynsuJo";

            string text = MB.Util.DESDataEncrypt.DecryptString(val2);
            string txtVal = MB.Util.DESDataEncrypt.DecryptString(val3);
            

            bool b = MB.Aop.SoftRegistry.AuthHelper.AuthRight("UFProjectSoft");
            if (b)
                Console.WriteLine("注册成功！");
            else
                Console.WriteLine("请输入正确的授权码！");

           

            Console.ReadLine();
        }


    }
}
