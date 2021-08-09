
using System;

using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;

namespace tictactoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        // Holds current results of cells in an active game
        
        private MarkType[] mResults;
        
        // True if it is player 1's turn (x) or player 2's turn (0)
        
        private bool mPlayerTurn;
        
        // true if game has ended
        
        private bool mGameEnded;
        
       
       

        public MainWindow()
        {
            InitializeComponent();
            NewGame();

        }
        
        
        
       private void NewGame()
        {
            // create a new blank array
            mResults = new MarkType[9];

            for(var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;
                
            
            //first players turn
                mPlayerTurn = true;

            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.Yellow;
                Button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;

            


        }


        /// handles a button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;
            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);


           
           if( mResults[index] != MarkType.Free) { return; }

            //Set the cell vale based on turn
            mResults[index] = mPlayerTurn ? MarkType.Cross : MarkType.Nought;
            button.Content = mPlayerTurn ? "X" : " O";
            if (mPlayerTurn) { mPlayerTurn = false; }
            else { mPlayerTurn = true; }
            if (!mPlayerTurn)
            {
                button.Foreground = Brushes.Green;
            }
            // you can also do mPlayerTurn ^= true

            //Check for a winner
            CheckForWinner();
        }


        private void CheckForWinner() { 
            //check for horizintal wins
            if(mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2] ) == mResults[0])
            {
                mGameEnded = true;

                       Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }

            // vertical
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }
            //Diagonal
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }




            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    
                    Button.Background = Brushes.Orange;
                    
                });


            }

        }

    }
}
