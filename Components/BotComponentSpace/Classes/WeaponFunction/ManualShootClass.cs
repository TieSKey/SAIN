﻿using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAIN.SAINComponent.Classes.WeaponFunction
{
    public class ManualShootClass : SAINBase
    {
        public ManualShootClass(BotComponent bot) : base(bot) { }

        public void Update()
        {
            if (Reason != EShootReason.None && 
                (!BotOwner.WeaponManager.HaveBullets || _timeStartManualShoot + 2f < Time.time || !BotOwner.ShootData.Shooting))
            {
                TryShoot(false, Vector3.zero);
            }
        }

        public bool TryShoot(bool value, Vector3 targetPos, bool checkFF = true, EShootReason reason = EShootReason.None)
        {
            ShootPosition = targetPos;
            Reason = value ? reason : EShootReason.None;

            if (value)
            {
                if (!CanShoot(checkFF))
                {
                    Reason = EShootReason.None;
                    return false;
                }

                Bot.Steering.LookToPoint(targetPos);
                if (Shooting)
                {
                    return false;
                }
                if (Bot.Steering.AngleToPointFromLookDir(targetPos) > 5)
                {
                    return false;
                }
                if (BotOwner.ShootData.Shoot())
                {
                    _timeStartManualShoot = Time.time;
                    return true;
                }
                return false;
            }
            BotOwner.ShootData.EndShoot();
            Reason = EShootReason.None;
            return false;
        }

        public bool Shooting => BotOwner.ShootData.Shooting;

        public bool CanShoot(bool checkFF = true)
        {
            if (checkFF && !Bot.FriendlyFireClass.ClearShot)
            {
                BotOwner.ShootData.EndShoot();
                return false;
            }
            BotWeaponManager weaponManager = BotOwner.WeaponManager;
            if (weaponManager.IsMelee)
            {
                return false;
            }
            if (!weaponManager.IsWeaponReady)
            {
                return false;
            }
            if (weaponManager.Reload.Reloading)
            {
                return false;
            }
            if (!BotOwner.ShootData.CanShootByState)
            {
                return false;
            }
            if (!weaponManager.HaveBullets)
            {
                return false;
            }
            return true;
        }

        private float _timeStartManualShoot;

        public Vector3 ShootPosition { get; private set; }

        public EShootReason Reason { get; private set; }

    }
}
