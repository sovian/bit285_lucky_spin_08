using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player
    {

        public long Id { get; set; } //all Entity Models have an Id
        public String FirstName { get; set; }
        public int Luck { get; set; }
        public Decimal Balance { get; set; }
        public ICollection<Spin> Spins { get; set; }

        //TODO: Add a Navigation Property representing the Player's Spins

    }
}