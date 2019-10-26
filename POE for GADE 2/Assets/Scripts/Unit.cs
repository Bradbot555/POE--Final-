using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GADE6112___Task3
{
    class Unit : Target
    {
        protected int speed;
        protected int attack;
        protected int attackRange;
        protected char symbol;
        protected bool isAttacking = false;
        protected string name;

        public Unit(int x, int y, int health, int speed, int attack, int attackRange, string faction, string name) {
 
            this.health = health;
            maxHealth = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.name = name;

            this.x = x;
            this.y = y;
            this.faction = faction;
            
        }

        public string Name {
            get { return name; }
        }

        public int Speed {
            get { return speed; }
        }

        //returns true if target was destroyed
        public virtual bool Attack(Target target) {
            isAttacking = true;
            target.Health -= attack;

            if (target.Health <= 0) {
                target.Health = 0;
                target.Destroy();
                return true;
            }

            return false;
        }

        public override void Destroy() {
            Health = 0;
            isDestroyed = true;
            isAttacking = false;
        }

        public virtual bool IsInRange(Target target) {
            return GetDistance(target) <= attackRange;
        }

        public virtual void Move(Target closestTarget) {
            int xDirection = closestTarget.X - X;
            int yDirection = closestTarget.Y - Y;

            if (Math.Abs(xDirection) > Math.Abs(yDirection))  {
                x += Math.Sign(xDirection);
            }
            else {
                y += Math.Sign(yDirection);
            }
        }

        public virtual void RunAway() {
            int direction = UnityEngine.Random.Range(0, 4);
            if (direction == 0) {
                x += 1;
            }
            else if (direction == 1) {
                x -= 1;
            }
            else if (direction == 2) {
                y += 1;
            }
            else {
                y -= 1;
            }
        }

        public override string ToString() {
            return
                "------------------------------------------" + Environment.NewLine +
                name + " (" + symbol + "/" + faction[0] + ")" + Environment.NewLine +
                "------------------------------------------" + Environment.NewLine +
                "Faction: " + faction + Environment.NewLine +
                "Position: " + x + ", " + y + Environment.NewLine +
                "Health: " + health + " / " + maxHealth + Environment.NewLine;
        }

    }
}
