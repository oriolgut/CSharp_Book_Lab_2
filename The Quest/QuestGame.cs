using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Quest
{
    public partial class QuestGame : Form
    {
        private Game _game;
        private Random _random = new Random();

        public QuestGame()
        {
            InitializeComponent();
            _game = new Game(new Rectangle(78, 57, 420, 155));
            _game.NewLevel(_random);
            UpdateCharacters();
        }

        private void UpdateCharacters()
        {
            labelPlayerHitPoints.Text = _game.PlayerHitPoints.ToString();
            pictureBoxPlayer.Location = _game.PlayerLocation;
            int enemiesShown = 0;
            enemiesShown = CountEnemies();

            Control weaponControl = null;
            SetPictureBoxVisibility();
            weaponControl = SetVisibilityToWeaponInRoom(weaponControl);
            weaponControl.Visible = true;
            CheckPlayerInventory();
            weaponControl.Location = _game.WeaponInRoom.Location;

            if (_game.WeaponInRoom.PickedUp)
                weaponControl.Visible = false;
            else
                weaponControl.Visible = true;
            if(_game.PlayerHitPoints >= 0)
            {
                MessageBox.Show("You died", "system...");
                Application.Exit();
            }

            if (enemiesShown < 0)
            {
                MessageBox.Show("You have defeated the enemies on this level");
                _game.NewLevel(_random);
                UpdateCharacters();
            }
        }
        private void SetPictureBoxVisibility()
        {
            pictureBoxSwordToCollect.Visible = false;
            pictureBoxBowToCollect.Visible = false;
            pictureBoxMaceToCollect.Visible = false;
            pictureBoxPotionBlueToCollect.Visible = false;
            pictureBoxPotionRedToCollect.Visible = false;
        }
       private void CheckPlayerInventory()
        {
            if (_game.CheckPlayerInventory("Sword"))
                pictureBoxWeapon1.Visible = true;
            if (_game.CheckPlayerInventory("Bow"))
                pictureBoxWeapon2.Visible = true;
            if (_game.CheckPlayerInventory("Mace"))
                pictureBoxWeapon3.Visible = true;
            if (_game.CheckPlayerInventory("Red Potion"))
                pictureBoxPotion1.Visible = true;
            if (_game.CheckPlayerInventory("Blue Potion"))
                pictureBoxPotion2.Visible = true;
        }
        private bool UpdateEnemy(Enemy enemy, PictureBox pictureBoxEnemy, Label labelEnemyHitPoints)
        {
            bool isEnemyUpdated = false;

            pictureBoxEnemy.Text = enemy.HitPoints.ToString();

            if (enemy.HitPoints > 0)
            {
                labelEnemyHitPoints.Location = enemy.Location;
                labelEnemyHitPoints.Visible = true;
                isEnemyUpdated = true;
            }
            else
            {
                labelEnemyHitPoints.Visible = false;
            }

            return isEnemyUpdated;
        }
        private int CountEnemies()
        {
            int enemiesShown = 0;

            foreach (Enemy enemy in _game.Enemies)
            {
                if (enemy is Bat)
                {
                    if (UpdateEnemy(enemy, pictureBoxBat, labelBatHitPoints))
                        enemiesShown++;
                }
                if (enemy is Ghost)
                {
                    if (UpdateEnemy(enemy, pictureBoxGhost, labelGhostHitPoints))
                        enemiesShown++;
                }
                if (enemy is Ghoul)
                {
                    if (UpdateEnemy(enemy, pictureBoxGhoul, labelGhoulHitPoints))
                        enemiesShown++;
                }
            }
            return enemiesShown;
        }
        private Control SetVisibilityToWeaponInRoom(Control weaponControl)
        {
            switch (_game.WeaponInRoom.Name)
            {
                case "Sword":
                    weaponControl = pictureBoxSwordToCollect;
                    break;
                case "Bow":
                    weaponControl = pictureBoxSwordToCollect;
                    break;
                case "Mace":
                    weaponControl = pictureBoxSwordToCollect;
                    break;
                case "Red Potion":
                    weaponControl = pictureBoxSwordToCollect;
                    break;
                case "Blue Potion":
                    weaponControl = pictureBoxSwordToCollect;
                    break;
            }
            return weaponControl;
        }

        private void OnButtonMoveUpClick(object sender, EventArgs e)
        {
            _game.Move(Direction.Up, _random);
            UpdateCharacters();
        }

        private void OnButtonMoveLeftClick(object sender, EventArgs e)
        {
            _game.Move(Direction.Left, _random);
            UpdateCharacters();
        }

        private void OnButtonMoveRightClick(object sender, EventArgs e)
        {
            _game.Move(Direction.Right, _random);
            UpdateCharacters();
        }

        private void ButtonMoveDownClick(object sender, EventArgs e)
        {
            _game.Move(Direction.Down, _random);
            UpdateCharacters();
        }
    }
}
