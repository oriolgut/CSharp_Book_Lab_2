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

        private void OnButtonMoveDownClick(object sender, EventArgs e)
        {
            _game.Move(Direction.Down, _random);
            UpdateCharacters();
        }

        private void OnPictureBoxWeapon1Click(object sender, EventArgs e)
        {
            SelectInventoryItem(pictureBoxWeapon1, "Sword", "Weapon");
            UpdateCharacters();
        }

        private void OnPictureBoxWeapon2Click(object sender, EventArgs e)
        {
            SelectInventoryItem(pictureBoxWeapon2, "Bow", "Weapon");
            UpdateCharacters();
        }

        private void OnPictureBoxWeapon3Click(object sender, EventArgs e)
        {
            SelectInventoryItem(pictureBoxWeapon3, "Mace", "Weapon");
            UpdateCharacters();
        }

        private void OnPictureBoxPotion1Click(object sender, EventArgs e)
        {
            SelectInventoryItem(pictureBoxPotion1, "Red Potion", "Potion");
            UpdateCharacters();
        }

        private void OnPictureBoxPotion2Click(object sender, EventArgs e)
        {
            SelectInventoryItem(pictureBoxPotion2, "Blue Potion", "Potion");
            UpdateCharacters();
        }

        private void buttonAttackUp_Click(object sender, EventArgs e)
        {
            _game.Attack(Direction.Up, _random);
            UpdateCharacters();
        }

        private void OnButtonAttackRightClick(object sender, EventArgs e)
        {
            _game.Attack(Direction.Right, _random);
            UpdateCharacters();
        }

        private void OnButtonAttackDownClick(object sender, EventArgs e)
        {
            _game.Attack(Direction.Down, _random);
            UpdateCharacters();
        }

        private void OnButtonAttackLeftClick(object sender, EventArgs e)
        {
            _game.Attack(Direction.Left, _random);
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
            if(_game.PlayerHitPoints <= 0)
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
        private void SelectInventoryItem(PictureBox item, string itemName, string weaponType)
        {
            if (_game.CheckPlayerInventory(itemName))
            {
                _game.Equip(itemName);
                RemoveInventoryBorders();
                item.BorderStyle = BorderStyle.FixedSingle;
                SetupAttackButtons(weaponType);
            }
        }
        private void RemoveInventoryBorders()
        {
            pictureBoxWeapon1.BorderStyle = BorderStyle.None;
            pictureBoxWeapon2.BorderStyle = BorderStyle.None;
            pictureBoxWeapon3.BorderStyle = BorderStyle.None;
            pictureBoxPotion1.BorderStyle = BorderStyle.None;
            pictureBoxPotion2.BorderStyle = BorderStyle.None;
        }
        private void SetupAttackButtons(string weaponType)
        {
            if ("weapon".Equals(weaponType.ToLower()))
            {
                buttonAttackUp.Text = "Up";
                buttonAttackDown.Visible = true;
                buttonAttackLeft.Visible = true;
                buttonAttackRight.Visible = true;
            } else if ("potion".Equals(weaponType.ToLower()))
            {
                buttonAttackUp.Text = "Drink";
                buttonAttackDown.Visible = false;
                buttonAttackLeft.Visible = false;
                buttonAttackRight.Visible = false;
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
                    weaponControl = pictureBoxBowToCollect;
                    break;
                case "Mace":
                    weaponControl = pictureBoxMaceToCollect;
                    break;
                case "Red Potion":
                    weaponControl = pictureBoxPotionRedToCollect;
                    break;
                case "Blue Potion":
                    weaponControl = pictureBoxPotionBlueToCollect;
                    break;
            }
            return weaponControl;
        }
    }
}
