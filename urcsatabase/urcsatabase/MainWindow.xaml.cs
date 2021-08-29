using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Media;

namespace urcsatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        bool moveLeft, moveRight;
        List<Rectangle> itemRemover = new List<Rectangle>();
        
        Random rand = new Random();

        int enemySpriteCounter = 0;
        int enemyCounter = 100;
        int playerSpeed = 10;
        int limit = 20;
        int score = 0;
        int damage = 0;
        int enemySpeed = 8;
        int highscore = 0;
        int special = 0;
        int bossdmg = 0;
        int bossspeed = 6;
        int bosslimit = 1;
        bool paused = false;
        bool right = false;
        bool left = true;
        int bulletlimit = 6;
        int bulletcounter = 100;
        int bulletspeed = 8;
        bool audio = true;
        int[] audiocount = new int[100];
        bool soundFinished = true;

        Rect playerHitBox;

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;

            

            MyCanvas.Focus();

            ImageBrush bg = new ImageBrush();

            bg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            MyCanvas.Background = bg;


            
            

            ImageBrush playerImage = new ImageBrush();
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            player.Fill = playerImage;
            StreamReader h = new StreamReader("scorelog.txt");
            int s = 0;
            int[] pontok = new int[500];
            while (!h.EndOfStream)
            {
                pontok[s] = Convert.ToInt32(h.ReadLine());
                s++;
            }
            for (int i = 0; i < s; i++)
            {
                if (pontok[i] > highscore)
                {
                    highscore = pontok[i];
                }
            }
            highscoreText.Content = "HIGH SCORE: " + highscore;
            if (score > highscore)
            {
                highscoreText.Content = "HIGH SCORE: " + score;
            }

        }
        private void OnClick3(object sender, EventArgs e)
        {
            if (audio == true)
            {
                audio = false;
                musicButton.Content = "Music: OFF";
            }
            else if (audio == false)
            {
                audio = true;
                musicButton.Content = "Music: ON";
            }
        }


        private void OnClick(object sender, EventArgs e)
        {
            playButton.Visibility = Visibility.Collapsed;
            musicButton.Visibility = Visibility.Collapsed;
            gameTimer.Start();
            special = rand.Next(1, 1000);
            
            
            for (int i = 0; i < 100; i++)
            {
                audiocount[i] = rand.Next(1, 5);
            }

            for (int i = 0; i < 10; i++)
            {
                if (audio == true)
                {
                    if (special == rand.Next(1, 1000))
                    {
                        SoundPlayer player2 = new SoundPlayer("C:/Users/peter/source/repos/urcsatabase/urcsatabase/images/audio6.wav");
                        player2.Load();
                        player2.Play();
                    }
                    else
                    {
                        /*SoundPlayer player2 = new SoundPlayer("C:/Users/peter/source/repos/urcsatabase/urcsatabase/images/audio" + audiocount[i] + ".wav");
                        soundFinished = false;
                        Task.Factory.StartNew(() => { player2.PlaySync(); soundFinished = true; });*/
                        SoundPlayer player2 = new SoundPlayer("C:/Users/peter/source/repos/urcsatabase/urcsatabase/images/theme.wav");
                        player2.PlayLooping();
                    }
                }
                
            }
        }
        private void OnClick2(object sender, EventArgs e)
        {
            restartButton.Visibility = Visibility.Collapsed;
            bossdamageText.Visibility = Visibility.Hidden;
            endText.Visibility = Visibility.Hidden;
            damageText.Foreground = Brushes.White;
            StreamReader h = new StreamReader("scorelog.txt");
            int s = 0;
            int[] pontok = new int[500];
            while (!h.EndOfStream)
            {
                pontok[s] = Convert.ToInt32(h.ReadLine());
                s++;
            }
            for (int i = 0; i < s; i++)
            {
                if (pontok[i] > highscore)
                {
                    highscore = pontok[i];
                }
            }
            highscoreText.Content = "HIGH SCORE: " + highscore;
            if (score > highscore)
            {
                highscoreText.Content = "HIGH SCORE: " + score;
            }

            enemySpriteCounter = 0;
            enemyCounter = 100;
            enemySpeed = 8;
            playerSpeed = 10;
            limit = 20;
            damage = 0;
            score = 0;
            bosslimit = 1;
            bossdmg = 0;
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && ((string)x.Tag == "enemy" || (string)x.Tag == "boss") || (string)x.Tag == "bossbullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) > -100)
                    {
                        itemRemover.Add(x);
                        
                    }

                }
            }
            /*if (audio == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (soundFinished)
                    {
                    SoundPlayer player2 = new SoundPlayer("C:/Users/peter/source/repos/urcsatabase/urcsatabase/images/audio" + audiocount[i] + ".wav");
                    soundFinished = false;
                    Task.Factory.StartNew(() => { player2.PlaySync(); soundFinished = true; });
                    }
                }
                
            }*/
            

            gameTimer.Start();
            
            
        }
        private void GameLoop(object sender, EventArgs e)
        {


            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);


            enemyCounter -= 1;
            bulletcounter -= 1;



            scoreText.Content = "Score: " + score;
            damageText.Content = "Life: " + (100 - damage);
            highscoreText.Content = "HIGH SCORE: " + highscore;
            if (score > highscore)
            {
                highscoreText.Content = "NEW HIGH SCORE: " + score;
            }

            if (enemyCounter < 0 && score != 57)
            {
                MakeEnemies();
                enemyCounter = limit;
            }
            if (bulletcounter < 0 && score == 57)
            {
                makeBullet();
                bulletcounter = bulletlimit;
            }

            if (moveLeft == true && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }


            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemRemover.Add(x);
                    }

                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            

                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                score++;
                            }
                        }
                    }
                    foreach (var y in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "boss")
                        {
                            Rect bossHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (bulletHitBox.IntersectsWith(bossHit))
                            {
                                itemRemover.Add(x);
                                bossdmg += 10;
                            }
                            if (bossdmg == 200)
                            {
                                itemRemover.Add(y);
                                bossdamageText.Visibility = Visibility.Hidden;
                                score++;

                            }
                            

                        }

                    }
                    

                }
                if (x is Rectangle && (string)x.Tag == "boss")
                {
                    if (Canvas.GetLeft(x) < 5)
                    {
                        left = false;
                        right = true;
                    }
                    if (Canvas.GetLeft(x) > 420)
                    {
                        right = false;
                        left = true;
                    }
                    if (left == true)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - bossspeed);
                    }
                    if (right == true)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + bossspeed);
                    }

                    
                }
                
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);

                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);
                        damage += 10;
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        damage += 5;
                        


                    }

                }
                if (x is Rectangle && (string)x.Tag == "bossbullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + bulletspeed);
                    
                    if (Canvas.GetTop(x) > 750)
                    {
                        itemRemover.Add(x);

                    }
                    Rect BulletHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(BulletHitbox))
                    {
                        damage += 10;
                        itemRemover.Add(x);



                    }
                    if (bossdmg == 200)
                    {
                        itemRemover.Add(x);
                    }
                }

                
                
            }

            foreach (Rectangle i in itemRemover)
            {
                MyCanvas.Children.Remove(i);
            }




            if (score > 10)
            {
                limit = 18;
                enemySpeed = 10;
            }

            if (score > 30)
            {
                limit = 15;
                enemySpeed = 12;
            }
            if (score > 50)
            {
                limit = 12;
                enemySpeed = 15;
            }
            if (score > 80)
            {
                limit = 10;
                enemySpeed = 19;
            }
            if (score > 100)
            {
                limit = 8;
                enemySpeed = 20;
            }
            if (score == 57)
            {
                bossdamageText.Visibility = Visibility.Visible;
                bossdamageText.Content = "Boss's life: " + (200 - bossdmg);
                bossdamageText.Foreground = Brushes.Red;
                

                foreach (var x in MyCanvas.Children.OfType<Rectangle>())
                {
                    if (x is Rectangle && (string)x.Tag == "enemy")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                        if (Canvas.GetTop(x) > -100)
                        {
                            itemRemover.Add(x);

                        }

                    }
                }
                if (bosslimit != 0)
                {
                    MakeBoss();
                    bosslimit = 0;
                    
                }
                


            }

            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Life: 0";
                damageText.Foreground = Brushes.Red;

                StreamWriter f = new StreamWriter("scorelog.txt", true);
                f.WriteLine(score);
                f.Close();
                 
                if (score > highscore)
                {
                    endText.Content = "NEW HIGH SCORE!\nYOU DIED\nYou exterminated " + score + " enemies!";
                }
                else
                {
                    endText.Content = "YOU DIED\nYou exterminated " + score + " enemies!";
                }
                endText.Visibility = Visibility.Visible;
                restartButton.Visibility = Visibility.Visible;
            }


        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
            
            
        }


        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
            if (e.Key == Key.Space)
            {
                if (paused == true)
                {
                    gameTimer.Start();
                    paused = false;
                    bossdamageText.Visibility = Visibility.Hidden;
                }
                ImageBrush newBullet = new ImageBrush();
                newBullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/6.png"));
                Rectangle newBullet2 = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 15,
                    Fill = newBullet,
                    

                };
                

                Canvas.SetLeft(newBullet2, Canvas.GetLeft(player) + player.Width / 2);
                Canvas.SetTop(newBullet2, Canvas.GetTop(player) - newBullet2.Height);


                MyCanvas.Children.Add(newBullet2);
            }
            if (e.Key == Key.Escape)
            {
                paused = true;
                
                bossdamageText.Visibility = Visibility.Visible;
                bossdamageText.Content = "Press Space to resume the game!";
                bossdamageText.Foreground = Brushes.Red;
            }
            if (paused == true)
            {
                gameTimer.Stop();
            }


        }

        private void MakeEnemies()
        {
            ImageBrush enemySprite = new ImageBrush();

            enemySpriteCounter = rand.Next(1, 5);
            enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/"+ enemySpriteCounter + ".png"));
            

            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite
            };

            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));
            MyCanvas.Children.Add(newEnemy);

        }
        


        private void MakeBoss()
        {
            ImageBrush boss = new ImageBrush();


            boss.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));


            Rectangle newBoss = new Rectangle
            {
                Tag = "boss",
                Height = 100,
                Width = 112,
                Fill = boss
            };
            Canvas.SetTop(newBoss, 40);
            Canvas.SetLeft(newBoss, rand.Next(5, 430));
            MyCanvas.Children.Add(newBoss);
        }
        private void makeBullet()
        {
            ImageBrush newBullet = new ImageBrush();
            newBullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/7.png"));
            Rectangle newBullet3 = new Rectangle
            {
                Tag = "bossbullet",
                Height = 20,
                Width = 15,
                Fill = newBullet,


            };
            Canvas.SetLeft(newBullet3, rand.Next(5, 480));
            Canvas.SetTop(newBullet3, 90 + newBullet3.Height);


            MyCanvas.Children.Add(newBullet3);
        }
    }
}
