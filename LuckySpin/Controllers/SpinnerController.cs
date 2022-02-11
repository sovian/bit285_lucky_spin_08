using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinContext _dbc;
        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinContext luckySpinContext)
        {
            _dbc = luckySpinContext;
        }

        /***
         * Entry Page Action
         **/

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel info)
        {
            if (!ModelState.IsValid) { return View(); }

            //Save Player in Repository
            Player player = new Player
            {
                FirstName = info.FirstName,
                Luck = info.Luck,
                Balance = info.StartingBalance
            };
            _dbc.Players.Add(player);
            _dbc.SaveChanges();
           
            return RedirectToAction("Spin", new { id = player.Id } );
        }

        /***
         * Game play through one Spin
         **/  
         [HttpGet]      
         public IActionResult Spin(long id)
        {
            //** Gets the Player belonging to the given id
            //TODO: Modify the code to use the SingleOrDefault Lamda Extension method
            //       Including the Player's Spins
            Player player = _dbc.Players.Include(p=>p.Spins).Single(p=>p.Id==id);

            // Populates a new SpinViewModel for this spin
            // using the player information
            SpinViewModel spinVM = new SpinViewModel() {
                FirstName = player.FirstName,
                Luck = player.Luck,
                Balance = player.Balance
            };

            //Checks if enough balance to play, if not drop out to LuckList
            if (!spinVM.ChargeSpin())
            {
                return RedirectToAction("LuckList", new { id = player.Id });
            }

            // Checks for Winnings
            if (spinVM.Winner) { spinVM.CollectWinnings(); }

            //** Updates Player Balance
            player.Balance = spinVM.Balance;
        
            Spin spin = new Spin()
            {
                IsWinning = spinVM.Winner
            };

            //** Adds the Spin to the Database Context
            //TODO: AFTER answering Question 3, modify the next line to use the Player's Spin collection instead of a global Spin
            //_dbc.Spins.Add(spin);
            player.Spins.Add(spin);
            //**** Saves all the changes to the Database at once
            _dbc.SaveChanges();

            return View("Spin", spinVM);
        }

        /***
         * ListSpins Action
         **/
         [HttpGet]
         public IActionResult LuckList(long id)
        {
            //Gets the Player belonging to the given id
            //TODO: Modify the code to use the SingleOrDefault Lamda Extension method
            Player player = _dbc.Players.Include(p=>p.Spins).Single(p=>p.Id==id);
            //Gets the list of Spins from the Context
            //TODO: Modify the next line to get the list of the Player's Spins instead of all the Spins
            IEnumerable<Spin> spins = player.Spins;
            // Hack in some detail about the player
            ViewBag.Player = player;

            return View(spins);
        }

    }
}

