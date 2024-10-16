using System;
using System.Collections.Generic;

class Program
{

    enum GuessResult { TooLow, TooHigh, Correct }

    struct Guess
    {
        public int Number;
        public GuessResult Result;
    }

    static void Main(string[] args)
    {
      
        int maxAttempts = 5;
        int secretNumber = new Random().Next(1, 101); 
        int attempts = 0;
        List<Guess> guessHistory = new List<Guess>(); 
      
        Dictionary<string, int> stats = new Dictionary<string, int>() { {"Wins", 0}, {"Losses", 0} }; 

        double playerScore = 0; 
        double scoreIncrement = 20.0;

        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine($"You have {maxAttempts} attempts to guess the number between 1 and 100.");

        while (attempts < maxAttempts)
        {
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();
            int guess;

            if (!int.TryParse(input, out guess))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue; 
            }

            GuessResult result = CheckGuess(guess, secretNumber);
            guessHistory.Add(new Guess { Number = guess, Result = result });

            attempts++;

            switch (result)
            {
                case GuessResult.Correct:
                    Console.WriteLine("Congratulations! You guessed the correct number!");
                    playerScore += scoreIncrement;
                    stats["Wins"]++;
                    break;

                case GuessResult.TooLow:
                    Console.WriteLine("Your guess is too low.");
                    break;

                case GuessResult.TooHigh:
                    Console.WriteLine("Your guess is too high.");
                    break;
            }

            if (result == GuessResult.Correct)
                break;

            Console.WriteLine($"You have {maxAttempts - attempts} attempts left.");
        }

        if (attempts == maxAttempts && guessHistory[guessHistory.Count - 1].Result != GuessResult.Correct)
        {
            Console.WriteLine("Sorry, you've used all your attempts. Better luck next time!");
            Console.WriteLine($"The correct number was {secretNumber}.");
            stats["Losses"]++;
        }

        DisplayGuessHistory(guessHistory);
        Console.WriteLine($"Your score: {playerScore}");
        Console.WriteLine($"Games Won: {stats["Wins"]}, Games Lost: {stats["Losses"]}");
    }

    static GuessResult CheckGuess(int guess, int secretNumber)
    {
        if (guess == secretNumber)
            return GuessResult.Correct;
        else if (guess < secretNumber)
            return GuessResult.TooLow;
        else
            return GuessResult.TooHigh;
    }

    static void DisplayGuessHistory(List<Guess> guessHistory)
    {
        Console.WriteLine("\nYour Guess History:");
        foreach (var guess in guessHistory)
        {
            Console.WriteLine($"Guess: {guess.Number}, Result: {guess.Result}");
        }
    }
}
