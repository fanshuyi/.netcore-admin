using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models.BlockModels
{
    public class Block
    {
        [Key]
        /// <summary>
        /// 区块位置
        /// </summary> 
        public int Index { get; set; }

        /// <summary>
        /// 区块生成时间戳
        /// </summary>
        [Timestamp]
        public long TimeStamp { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 区块 SHA-256 散列值
        /// </summary>
        [MaxLength(256)]
        public string Hash { get; set; }

        /// <summary>
        /// 前一个区块 SHA-256 散列值
        /// </summary>
        [MaxLength(256)]
        public string PrevHash { get; set; }

        /// <summary>
        /// 下一个区块生成难度
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// 随机值
        /// </summary>
        public string Nonce { get; set; }
    }      

}
