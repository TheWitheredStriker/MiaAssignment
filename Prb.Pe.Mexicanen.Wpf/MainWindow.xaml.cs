using System;
using System.Linq;
using System.Windows;

// TODO
// Reset throws counter in every round
// Ensure that the buttons REMAIN disabled at end of game until you hit the button to restart
// Restart button

namespace Prb.Pe.Mexicanen.Wpf 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window 
    {
        int         p1StartScore    = 6,
                    p2StartScore    = 6,
                    p1NumOfThrows   = 3,
                    p2NumOfThrows   = 3,
                    p1DiceVal1,
                    p1DiceVal2,
                    p2DiceVal1,
                    p2DiceVal2,
                    p1DiceScore,
                    p2DiceScore;

        bool        player1Turn     = true;

        // This value is not instantiated with the class
        static Random rand          = new Random(); // Var is not allowed in this part of the code
        
        public MainWindow() 
        { 
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI() 
        {
            lblPlayer1NumberOfThrows.Content   = p1NumOfThrows;
            lblPlayer2NumberOfThrows.Content   = p2NumOfThrows;
            lblPlayer1Dice1.Content            = p1DiceVal1;
            lblPlayer1Dice2.Content            = p1DiceVal2;
            lblPlayer2Dice1.Content            = p2DiceVal1;
            lblPlayer2Dice2.Content            = p2DiceVal2;
            lblPlayer1Score.Content            = p1StartScore;
            lblPlayer2Score.Content            = p2StartScore;
            lblPlayer1DiceScore.Content        = p1DiceScore;
            lblPlayer2DiceScore.Content        = p2DiceScore;

            // If Player1Turn is true, then so is btn... .IsEnabled.
            // Same goes for when Player1Turn is false. Both have the same value.

            btnThrowDicesPlayer1.IsEnabled     = player1Turn;
            btnThrowDicesPlayer2.IsEnabled     = !player1Turn;

            btnPlayer1KeepDiceResult.IsEnabled = player1Turn;
            btnPlayer2KeepDiceResult.IsEnabled = !player1Turn;

            btnResetGame.IsEnabled             = (p1StartScore == 0 || p2StartScore == 0);
        }

        private void btnThrowDicesPlayer1_Click(object sender, RoutedEventArgs e) 
        {
            p1DiceVal1  = rand.Next(1, 6);
            p1DiceVal2  = rand.Next(1, 6);
         
            p1DiceScore = CalculateDice(p1DiceVal1, p1DiceVal2);
            --p1NumOfThrows;
            UpdateUI();
        }

        private int CalculateDice(int num1, int num2) 
        {
            int   result;
            int[] theNumbers   = new int[] {num1, num2};

            if (num1 == num2)
                result         = num1 * 100;

            else if (theNumbers.Contains(1) && theNumbers.Contains(2)) 
                result         = 1000;

            else 
            {
                int highestNum = theNumbers.Max();
                int lowestNum  = theNumbers.Min();
                result         = (highestNum * 10) + lowestNum;
            }

            return result;
        }

        private void btnPlayer1KeepDiceResult_Click(object sender, RoutedEventArgs e) 
        {
            player1Turn       = false;
            UpdateUI();
        }

        private void btnThrowDicesPlayer2_Click(object sender, RoutedEventArgs e) 
        {
            p2DiceVal1  = rand.Next(1, 6);
            p2DiceVal2  = rand.Next(1, 6);
         
            p2DiceScore = CalculateDice(p2DiceVal1, p2DiceVal2);
            --p2NumOfThrows;
            UpdateUI();
        }

        private void btnPlayer2KeepDiceResult_Click(object sender, RoutedEventArgs e) 
        {

            UpdateUI();

            bool ifPlayer1Won = p1DiceScore > p2DiceScore; 

            if (ifPlayer1Won)
                --p2StartScore;
            
            else
                --p1StartScore;

            if (p1StartScore == 0 || p2StartScore == 0) 
            {
                btnResetGame.IsEnabled             = true;
                btnPlayer1KeepDiceResult.IsEnabled = false;
                btnPlayer2KeepDiceResult.IsEnabled = false;
                btnThrowDicesPlayer1.IsEnabled     = false;
                btnThrowDicesPlayer2.IsEnabled     = false;
                MessageBox.Show($"{(p1DiceScore == 0 ? "Speler 2" : "Speler 1")} is gewonnen!");
                btnPlayer1KeepDiceResult.IsEnabled = false;
                btnPlayer2KeepDiceResult.IsEnabled = false;
                btnThrowDicesPlayer1.IsEnabled     = false;
                btnThrowDicesPlayer2.IsEnabled     = false;
            }

            else
                player1Turn = true;
        }

    }
}
