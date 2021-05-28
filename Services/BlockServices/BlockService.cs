using IServices.IBlockServices;
using Models.BlockModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Services.BlockServices
{
   public  class BlockService: IBlockService
    {
        public static List<Block> _blockChain = new List<Block>();

        /// <summary>
        /// 计算区块 HASH 值
        /// </summary>
        /// <param name="block">区块实例</param>
        /// <returns>计算完成的区块散列值</returns>
        public static string CalculateHash(Block block)
        {
            string calculationStr = $"{block.Index}{block.TimeStamp}{block.Value}{block.PrevHash}{block.Nonce}";

            SHA256 sha256Generator = SHA256.Create();
            byte[] sha256HashBytes = sha256Generator.ComputeHash(Encoding.UTF8.GetBytes(calculationStr));
            StringBuilder sha256StrBuilder = new StringBuilder();
            foreach (byte @byte in sha256HashBytes)
            {
                sha256StrBuilder.Append(@byte.ToString("x2"));
            }
            return sha256StrBuilder.ToString();
        }

        /// <summary>
        /// 生成新的区块
        /// </summary>
        /// <param name="oldBlock">旧的区块数据</param>
        /// <param name="BPM">心率</param>
        /// <returns>新的区块</returns>
        public static Block GenerateBlock(Block oldBlock, string value)
        {
            Block newBlock = new Block()
            {
                Index = oldBlock.Index + 1,
                TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                Value = value,
                PrevHash = oldBlock.Hash,
                Difficulty = 1
            };

            // 挖矿 ing...
            for (int i = 0; ; i++)
            {
                newBlock.Nonce = i.ToString("x2");
                if (!IsHashValid(CalculateHash(newBlock), newBlock.Difficulty))
                {
                    Console.WriteLine($"目前结果：{CalculateHash(newBlock)} ，正在计算中...");
                    continue;
                }
                else
                {
                    Console.WriteLine($"目前结果：{CalculateHash(newBlock)} ，计算完毕...");
                    newBlock.Hash = CalculateHash(newBlock);
                    break;
                }
            }

            //            newBlock.Hash = CalculateHash(newBlock);
            return newBlock;
        }

        /// <summary>
        /// 检验区块是否有效
        /// </summary>
        /// <param name="newBlock">新生成的区块数据</param>
        /// <param name="oldBlock">旧的区块数据</param>
        /// <returns>有效返回 TRUE，无效返回 FALSE</returns>
        public static bool IsBlockValid(Block newBlock, Block oldBlock)
        {
            if (oldBlock.Index + 1 != newBlock.Index) return false;
            if (oldBlock.Hash != newBlock.PrevHash) return false;
            if (CalculateHash(newBlock) != newBlock.Hash) return false;
            return true;
        }

        /// <summary>
        /// 校验 Hash 是否有效
        /// </summary>
        /// <param name="hashStr">Hash 值</param>
        /// <param name="difficulty">难度</param>
        /// <returns></returns>
        public static bool IsHashValid(string hashStr, int difficulty)
        {
            var bytes = Enumerable.Range(0, hashStr.Length)
                .Where(n => n % 2 == 0)
                .Select(n => Convert.ToByte(hashStr.Substring(n, 2), 16))
                .ToArray();

            var bits = new BitArray(bytes);

            for (var i = 0; i < difficulty; i++)
            {
                if (bits[i]) return false;
            }

            return true;
        }

        /// <summary>
        /// 如果新的区块链比当前区块链更新，则切换当前区块链为最新区块链
        /// </summary>
        /// <param name="newBlockChain">新的区块链</param>
        public static void SwitchChain(List<Block> newBlockChain)
        {
            if (newBlockChain.Count > _blockChain.Count)
            {
                _blockChain = newBlockChain;
            }
        }
    }


}
