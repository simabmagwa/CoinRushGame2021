/*Simanye Buhle Magwa
 * December 2021
 * Coin Rush Game
 * This classic game was created with guidance from a youtuber named MOO ICT.
 * The main character(purple) has to try and collect all the coins(yellow) and reach the door(brown) inorder to win.
 * The main character has to avoid being  captured by the enemies[red] and avoid falling off the platforms[green] because that would result in game over
 * There are 2 moving platforms which the main character can use to it's aid , one moving horizontal and vertically.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinRushGame2021
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;
//speed platform(horizontal and vertical)
        int horizontalSpeed = 5;
        int verticalSpeed = 3;
        //speed of enemy 1&2
        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "score: " + score;
            player.Top += jumpSpeed;
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true&&force<0)
            {
                jumping = false;
            }
            if (jumping==true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds)) //check if player is interacting with bounds
                        {
                            force = 8;
                            player.Top = x.Top - player.Height; //wheneever player jumps on platform the player will be plaed on top of platform
                            if((string)x.Name=="horizontalPlatform"&&goLeft==false || (string) x.Name == "verticalPlatform" && goRight == false )
                            {
                                player.Left -= horizontalSpeed;
                            }

                        }
                        x.BringToFront();
                    }
                        if((string)x.Tag=="coin")
                        {
                           if(player.Bounds.IntersectsWith(x.Bounds)&&x.Visible==true)
                            {
                                x.Visible = false;
                                score++;

                            }
                        }
                        if((string)x.Tag=="enemy")
                    {
                        if(player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTmer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "GameOver!!"; //adds text when player has touched enemy and game is over


                        }
                    }
                    }
                }
            horizontalPlatform.Left -= horizontalSpeed;
            if(horizontalPlatform.Left<0||horizontalPlatform.Left+horizontalPlatform.Width>this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            verticalPlatform.Top += verticalSpeed;
            if (verticalPlatform.Top < 178 || verticalPlatform.Top > 552)
            {
                verticalSpeed = -verticalSpeed;
            }
            enemyOne.Left -= enemyOneSpeed;
            if(enemyOne.Left<pictureBox5.Left||enemyOne.Left+enemyOne.Width>pictureBox5.Left+pictureBox5.Width)//ensure enemies stay on platform
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left -= enemyTwoSpeed;
            if(enemyTwo.Left<pictureBox2.Left||enemyTwo.Left+enemyTwo.Width>pictureBox2.Left+pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }
            if(player.Top+player.Height>this.ClientSize.Height+50)
            {
                gameTmer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to the" +Environment.NewLine+"bottom! Game Over";
            }
            if(player.Bounds.IntersectsWith(door.Bounds)&&score==28)
            {
                gameTmer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Your quest is complete!";

            }
            
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode==Keys.Right)
            {
                goRight = true;
            }
            if(e.KeyCode==Keys.Space&&jumping==false)
            {
                jumping = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(jumping==true)
            {
                jumping = false;
            }
            if(e.KeyCode==Keys.Enter&&isGameOver==true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {

            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox&&x.Visible==false)
                {
                    x.Visible = true;
                }
            }

            //reset position of player,platform and enemies

            player.Left =85;
            player.Top =661;

            enemyOne.Left = 495;
            enemyTwo.Left = 380 ;

            horizontalPlatform.Left = 285;
            verticalPlatform.Top = 552;
            

           


        }

    }

}
