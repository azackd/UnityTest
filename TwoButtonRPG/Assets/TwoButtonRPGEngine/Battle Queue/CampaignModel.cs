using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Characters;

namespace Assets.TwoButtonRPGEngine.Battle_Queue
{
    public class CampaignModel
    {
        public List<BaseCharacter> Characters { get; set; }

        public CampaignModel()
        {
            Characters = new List<BaseCharacter>();

            Characters.Add(CharacterFighter.CreateCharacter(0));
            Characters.Add(CharacterCleric.CreateCharacter(1));
            Characters.Add(CharacterWizard.CreateCharacter(2));
            Characters.Add(CharacterRogue.CreateCharacter(3));
        }

    }
}
