﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeIceCream : ImpostorMod 
{
    [SerializeField]
    private TextMesh name, flavor; //SerializeField causes the variable to show up in the inspector, while keeping it a private variable.
    private static readonly string[] realNames = { "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob" };
    private static readonly string[] realFlavors = { "Cookies and Cream", "Neapolitan", "Tutti Frutti", "The Classic", "Rocky Road", "Double Chocolate", "Mint Chocolate Chip", "Double Strawberry", "Raspberry Ripple", "Vanilla" };
    private static readonly string[] fakeNames = { "Gort", "Jay", "Joey", "Robert!", "Jeb", "Deez", "Marc", "Blan", "Juan", "Chegg", "Dan", "You", "Deaf", "Kevin", "Bio", "Mario", "Wayne", "Claire", "Cthulu", "Teddy", "Red", "Dream", "Arnold", "Matt", "$4.99", "Saber", "Archie", "Epic", "Cunk", "Ngoc", "Bill", "Clemp", "Smilfs", "Tyler", "Jimmy", "Me", "Muck", "Obama", "Brady", "Chester", "Spot", "User", "Cookie", "Star", "Asolfo", "Plinko", "Cora", "Max", "Alex", "Millie", "Lily", "Peach", "Zaak" };
    private static readonly string[] fakeFlavors = { "Pistachio", "Ice", "Rocks", "Brick", "Boiling Water", "Hot Butter", "Ice Cream", "Malaria", "Bubble Gum", "Butter Pecan", "Sugar", "Pickle", "Moose Tracks", "Coffee", "Cunk", "Cookie Dough", "Cherry", "Mystery Flavor", "NullReferenceException", "Soylent Green", "Car Tires", "Colored Switches", "The Timwi Special", "Corn (inedible)", "Onion", "Steak Sauce", "Fear & Terror", "Estrogen", "Estrogen Estrogen", "Progesterone", "Money", "Olive Oil", "Crude Oil", "Rust", "Grunkle Squeaky", "Fish Sauce", "Hot Dog", "Sin", "Salt", "Muck", "Root Beer", "Sprite", "Sprite Cranberry", "Vape Juice", "Rhubarb", "Skittles", "Trash", "Meatball", "Tomato", "Ritz Pie Sundae", "Saltine Crackers", "Meat (w/ fruit dip)", "Concrete", "Avocado", "Pepperoni Pizza", "Gruyere", "Artisan", "Ghost Pepper", "Lighter Fluid", "Superman", "TV Static", "Baked Beans", "Neutrinos", "Strange Quarks", "Moyai", "Whale", "Turkey", "Finesse", "Cotton Candy", "A Rocky Road", "Bag of Chips", "Blan", "Glue", "Gunpowder", "Baln", "Shaving Cream", "Toenail Clippings", "Geometry Dash", "Ants", "Marbles", "Fire", "Glass", "Moldy Pizza", "Fox Urine", "Green Beans", "Licorice", "7", "Antifreeze", "Drain Cleaner", "Mr. Clean", "7-UP", "Cheeto", "Battery Acid", "Ecstacy", "Unspeakable Horror", "Clemped Smilfs", "Pills", "بوظة", "Souls of the Damned", "Catboy", "Dementia", "Annoying Orange Flavor", "$4.99", "Cheez-its", "Uranium-235", "Ketchup", "Horse Glue", "Cheezy Gordita Crun", "Blueberry", "Sweat", "Cement", "Windex", "Tom Brady's Son", "Hot Dog Water", "Creatine", "Left Sock", "Ham & Cheese", "Mint Jam", "Bellybutton Lint", "Wendy's®\nPretzel\nBacon\nPub\nChicken™", "Doorknob", "Penny", "Dog Food", "BlvdBroken", "Wheat Thins", "Male", "Mayonnaise", "Liquid Smoke", "Play-Doh", "Philly Cheesesteak", "Fish's Secret Sauce", "The Charlie Sierra", "Fish Food"};
    private int Case;
    void Start()
    {
        Case = Rnd.Range(0, 2); //However many cases you want there to be.
        Case = 0;
        if (Case == 0)
        {
            flickerObjs.Add(name.gameObject);
            name.text = fakeNames.PickRandom();
        }
        else name.text = realNames.PickRandom();
        if (Case == 1)
        {
            flickerObjs.Add(flavor.gameObject);
            flavor.text = fakeFlavors.PickRandom();
        }
        else flavor.text = realFlavors.PickRandom();
    }
}
