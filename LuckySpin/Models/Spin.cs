using System;
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Spin
    {
        public long Id { get; set; }
        public Boolean IsWinning { get; set; }

        //TODO: Nothing to do here, instead modify Player.cs to contain a collection of Spins
    }
}
