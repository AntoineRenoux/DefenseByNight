using System;
using System.Collections.Generic;
using System.Linq;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Helpers.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Tools.Enum;
using DefenseByNight.API.Helpers.Enums.Disciplines;
using DefenseByNight.API.Helpers.Enums.Clans;

namespace DefenseByNight.API.Data.Seeds
{

    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/Seeds/Users.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>{
                    new Role{Name = EnumRoles.ADMIN},
                    new Role{Name = EnumRoles.CA},
                    new Role{Name = EnumRoles.MEMBER},
                    new Role{Name = EnumRoles.ORGANIZER},
                    new Role{Name = EnumRoles.PLAYER},
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    user.CreatedDate = DateTime.Now;
                    userManager.CreateAsync(user, "password").Wait();
                    if (user.UserName.ToLower() == "batman")
                        userManager.AddToRolesAsync(user, new List<string> { EnumRoles.ADMIN, EnumRoles.MEMBER }).Wait();
                    else
                        userManager.AddToRoleAsync(user, EnumRoles.MEMBER).Wait();
                }
            }
        }

        public static void SeedChronicles(DataContext context)
        {
            if(!context.Chronicles.Any())
            {
                var chronicle = new Chronicle() {
                    Name = "La cité de Verre",
                    StartDate = new DateTime(2018, 9, 1),
                    EndDate = null,
                    City = "Ys",
                    BaseExperience = 100,
                    ExperiencePerMonth = 2
                };

                context.Chronicles.Add(chronicle);

                context.SaveChanges();
            }
        }

        public static void SeedReferences(DataContext context)
        {
            if (!context.References.Any())
            {
                var allRegerenceFiles = new string[]
                {
                    "Data/Seeds/References/Configuration.json"
                };

                foreach (var file in allRegerenceFiles)
                {
                    context.References.AddRange(JsonConvert.DeserializeObject<List<Reference>>(System.IO.File.ReadAllText(file)));
                }

                context.SaveChanges();
            }
        }

        public static void SeedTraductions(DataContext context)
        {
            if (!context.Traductions.Any())
            {
                var traductions = new List<Traduction>();
                var allTraductionFiles = new string[]
                    {
                        "Data/Seeds/Traductions/User_fr.json",
                        "Data/Seeds/Traductions/General_fr.json",
                        "Data/Seeds/Traductions/NavBar_fr.json",
                        "Data/Seeds/Traductions/ConnexionRegistrationEdition_fr.json",
                        "Data/Seeds/Traductions/Character_fr.json",
                        "Data/Seeds/Traductions/Roles_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Affilifation_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Focus_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Attribute_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Animalism_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Auspex_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Celerity_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Chimestry_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Daimonion_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Dementation_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Dominate_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Fortitude_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Melpominee_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Mytherceria_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Obeah_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Obfuscate_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Obtenebration_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Potence_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Presence_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Protean_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Quietus_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Serpentis_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Temporis_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Thanatosis_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Valeren_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Vicissitude_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Visceratika_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Atout_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Sepulcher_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Bones_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Asches_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Mortis_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Blood_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Conjuration_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Elements_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Fire_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Spirit_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Technology_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Weather_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Clans_fr.json",
                        "Data/Seeds/Traductions/RulesFromGame/Flaw_fr.json"
                    };

                foreach (var file in allTraductionFiles)
                {
                    traductions.AddRange(JsonConvert.DeserializeObject<List<Traduction>>(System.IO.File.ReadAllText(file)));
                }

                traductions.ForEach(trad =>
                    {
                        context.Traductions.Add(new Traduction
                        {
                            Key = trad.Key,
                            LCID = 1036,
                            Text = trad.Text
                        });
                    });

                context.SaveChanges();

            }
        }

        public static void SeedPhotos(DataContext context)
        {
            if (!context.Photos.Any())
            {
                context.Photos.Add(new Photo
                {
                    PublicId = "default-user-icon_rw8f4j",
                    Url = "https://res.cloudinary.com/dbf5hrvz3/image/upload/v1590170970/default-user-icon_rw8f4j.jpg",
                    DateAdded = DateTime.Now
                });

                context.SaveChanges();
            }
        }

        public static void SeedFlaws(DataContext context)
        {
            if (!context.Flaws.Any())
            {
                context.Flaws.AddRange(
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ADDICTION_KEY, Name = EnumAtoutFlaw.FLAW_ADDICTION_NAME, Description = EnumAtoutFlaw.FLAW_ADDICTION_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_AMNESIA_KEY, Name = EnumAtoutFlaw.FLAW_AMNESIA_NAME, Description = EnumAtoutFlaw.FLAW_AMNESIA_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ILLITERATE_KEY, Name = EnumAtoutFlaw.FLAW_ILLITERATE_NAME, Description = EnumAtoutFlaw.FLAW_ILLITERATE_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ARCHAIC_KEY, Name = EnumAtoutFlaw.FLAW_ARCHAIC_NAME, Description = EnumAtoutFlaw.FLAW_ARCHAIC_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_LINKED_TO_EARTH_KEY, Name = EnumAtoutFlaw.FLAW_LINKED_TO_EARTH_NAME, Description = EnumAtoutFlaw.FLAW_LINKED_TO_EARTH_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_UNHOLY_BEACON_KEY, Name = EnumAtoutFlaw.FLAW_UNHOLY_BEACON_NAME, Description = EnumAtoutFlaw.FLAW_UNHOLY_BEACON_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ONE_EYED_KEY, Name = EnumAtoutFlaw.FLAW_ONE_EYED_NAME, Description = EnumAtoutFlaw.FLAW_ONE_EYED_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_NIGHTMARE_KEY, Name = EnumAtoutFlaw.FLAW_NIGHTMARE_NAME, Description = EnumAtoutFlaw.FLAW_NIGHTMARE_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_CORPSE_MEAT_KEY, Name = EnumAtoutFlaw.FLAW_CORPSE_MEAT_NAME, Description = EnumAtoutFlaw.FLAW_CORPSE_MEAT_DESCRIPTION, Cost = 5 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_HUNTED_KEY, Name = EnumAtoutFlaw.FLAW_HUNTED_NAME, Description = EnumAtoutFlaw.FLAW_HUNTED_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_BLUNT_FANGS_KEY, Name = EnumAtoutFlaw.FLAW_BLUNT_FANGS_NAME, Description = EnumAtoutFlaw.FLAW_BLUNT_FANGS_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_CURIOSITY_KEY, Name = EnumAtoutFlaw.FLAW_CURIOSITY_NAME, Description = EnumAtoutFlaw.FLAW_CURIOSITY_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ATTENTION_DEFICIT_KEY, Name = EnumAtoutFlaw.FLAW_ATTENTION_DEFICIT_NAME, Description = EnumAtoutFlaw.FLAW_ATTENTION_DEFICIT_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SNOW_FLAKE_KEY, Name = EnumAtoutFlaw.FLAW_SNOW_FLAKE_NAME, Description = EnumAtoutFlaw.FLAW_SNOW_FLAKE_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SENSITIVE_STOMACH_KEY, Name = EnumAtoutFlaw.FLAW_SENSITIVE_STOMACH_NAME, Description = EnumAtoutFlaw.FLAW_SENSITIVE_STOMACH_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_BLAND_KEY, Name = EnumAtoutFlaw.FLAW_BLAND_NAME, Description = EnumAtoutFlaw.FLAW_BLAND_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_WEAKNESS_KEY, Name = EnumAtoutFlaw.FLAW_WEAKNESS_NAME, Description = EnumAtoutFlaw.FLAW_WEAKNESS_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_HAMELIN_FLUTIST_KEY, Name = EnumAtoutFlaw.FLAW_HAMELIN_FLUTIST_NAME, Description = EnumAtoutFlaw.FLAW_HAMELIN_FLUTIST_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_KID_KEY, Name = EnumAtoutFlaw.FLAW_KID_NAME, Description = EnumAtoutFlaw.FLAW_KID_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_HEAL_SLOWLY_KEY, Name = EnumAtoutFlaw.FLAW_HEAL_SLOWLY_NAME, Description = EnumAtoutFlaw.FLAW_HEAL_SLOWLY_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_HAUNTED_KEY, Name = EnumAtoutFlaw.FLAW_HAUNTED_NAME, Description = EnumAtoutFlaw.FLAW_HAUNTED_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_IMPATIENT_KEY, Name = EnumAtoutFlaw.FLAW_IMPATIENT_NAME, Description = EnumAtoutFlaw.FLAW_IMPATIENT_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_WHITE_WATER_KEY, Name = EnumAtoutFlaw.FLAW_WHITE_WATER_NAME, Description = EnumAtoutFlaw.FLAW_WHITE_WATER_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_INEPT_KEY, Name = EnumAtoutFlaw.FLAW_INEPT_NAME, Description = EnumAtoutFlaw.FLAW_INEPT_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_INFAMOUS_BROOD_KEY, Name = EnumAtoutFlaw.FLAW_INFAMOUS_BROOD_NAME, Description = EnumAtoutFlaw.FLAW_INFAMOUS_BROOD_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_BESTIAL_INSTINCT_KEY, Name = EnumAtoutFlaw.FLAW_BESTIAL_INSTINCT_NAME, Description = EnumAtoutFlaw.FLAW_BESTIAL_INSTINCT_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_INTOLERANCE_KEY, Name = EnumAtoutFlaw.FLAW_INTOLERANCE_NAME, Description = EnumAtoutFlaw.FLAW_INTOLERANCE_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_THE_BEAST_IN_THE_MIRROR_KEY, Name = EnumAtoutFlaw.FLAW_THE_BEAST_IN_THE_MIRROR_NAME, Description = EnumAtoutFlaw.FLAW_THE_BEAST_IN_THE_MIRROR_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_POSEIDON_KEY, Name = EnumAtoutFlaw.FLAW_POSEIDON_NAME, Description = EnumAtoutFlaw.FLAW_POSEIDON_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_HARD_HEARING_KEY, Name = EnumAtoutFlaw.FLAW_HARD_HEARING_NAME, Description = EnumAtoutFlaw.FLAW_HARD_HEARING_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_CURSED_KEY, Name = EnumAtoutFlaw.FLAW_CURSED_NAME, Description = EnumAtoutFlaw.FLAW_CURSED_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_POOR_EYESIGHT_KEY, Name = EnumAtoutFlaw.FLAW_POOR_EYESIGHT_NAME, Description = EnumAtoutFlaw.FLAW_POOR_EYESIGHT_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DIFFICULT_MOBILITY_KEY, Name = EnumAtoutFlaw.FLAW_DIFFICULT_MOBILITY_NAME, Description = EnumAtoutFlaw.FLAW_DIFFICULT_MOBILITY_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DEAD_BURIED_KEY, Name = EnumAtoutFlaw.FLAW_DEAD_BURIED_NAME, Description = EnumAtoutFlaw.FLAW_DEAD_BURIED_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_NECROPHIL_KEY, Name = EnumAtoutFlaw.FLAW_NECROPHIL_NAME, Description = EnumAtoutFlaw.FLAW_NECROPHIL_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_NEGLIGENT_KEY, Name = EnumAtoutFlaw.FLAW_NEGLIGENT_NAME, Description = EnumAtoutFlaw.FLAW_NEGLIGENT_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_NOTORIETY_KEY, Name = EnumAtoutFlaw.FLAW_NOTORIETY_NAME, Description = EnumAtoutFlaw.FLAW_NOTORIETY_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_FRAGILE_BONES_KEY, Name = EnumAtoutFlaw.FLAW_FRAGILE_BONES_NAME, Description = EnumAtoutFlaw.FLAW_FRAGILE_BONES_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_WEAK_GENERATION_KEY, Name = EnumAtoutFlaw.FLAW_WEAK_GENERATION_NAME, Description = EnumAtoutFlaw.FLAW_WEAK_GENERATION_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_PERMANENT_WOUND_KEY, Name = EnumAtoutFlaw.FLAW_PERMANENT_WOUND_NAME, Description = EnumAtoutFlaw.FLAW_PERMANENT_WOUND_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DISEASE_CARRIER_KEY, Name = EnumAtoutFlaw.FLAW_DISEASE_CARRIER_NAME, Description = EnumAtoutFlaw.FLAW_DISEASE_CARRIER_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SINISTER_PRESENCE_KEY, Name = EnumAtoutFlaw.FLAW_SINISTER_PRESENCE_NAME, Description = EnumAtoutFlaw.FLAW_SINISTER_PRESENCE_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DAMNED_KEY, Name = EnumAtoutFlaw.FLAW_DAMNED_NAME, Description = EnumAtoutFlaw.FLAW_DAMNED_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_PROPHET_GEHENNA_KEY, Name = EnumAtoutFlaw.FLAW_PROPHET_GEHENNA_NAME, Description = EnumAtoutFlaw.FLAW_PROPHET_GEHENNA_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_REJECTED_PREY_KEY, Name = EnumAtoutFlaw.FLAW_REJECTED_PREY_NAME, Description = EnumAtoutFlaw.FLAW_REJECTED_PREY_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_STOLEN_POTENTIAL_KEY, Name = EnumAtoutFlaw.FLAW_STOLEN_POTENTIAL_NAME, Description = EnumAtoutFlaw.FLAW_STOLEN_POTENTIAL_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_BAD_LOCK_HOLDER_KEY, Name = EnumAtoutFlaw.FLAW_BAD_LOCK_HOLDER_NAME, Description = EnumAtoutFlaw.FLAW_BAD_LOCK_HOLDER_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_OBVIOUS_PREDATOR_KEY, Name = EnumAtoutFlaw.FLAW_OBVIOUS_PREDATOR_NAME, Description = EnumAtoutFlaw.FLAW_OBVIOUS_PREDATOR_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_PRESUMPTUOUS_KEY, Name = EnumAtoutFlaw.FLAW_PRESUMPTUOUS_NAME, Description = EnumAtoutFlaw.FLAW_PRESUMPTUOUS_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SLOW_REACTIONS_KEY, Name = EnumAtoutFlaw.FLAW_SLOW_REACTIONS_NAME, Description = EnumAtoutFlaw.FLAW_SLOW_REACTIONS_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_OSTENTATIOUS_MEAL_KEY, Name = EnumAtoutFlaw.FLAW_OSTENTATIOUS_MEAL_NAME, Description = EnumAtoutFlaw.FLAW_OSTENTATIOUS_MEAL_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_REPELLED_RELIGION_KEY, Name = EnumAtoutFlaw.FLAW_REPELLED_RELIGION_NAME, Description = EnumAtoutFlaw.FLAW_REPELLED_RELIGION_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_CLEAR_BLOOD_KEY, Name = EnumAtoutFlaw.FLAW_CLEAR_BLOOD_NAME, Description = EnumAtoutFlaw.FLAW_CLEAR_BLOOD_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_ROTTEN_BLOOD_KEY, Name = EnumAtoutFlaw.FLAW_ROTTEN_BLOOD_NAME, Description = EnumAtoutFlaw.FLAW_ROTTEN_BLOOD_DESCRIPTION, Cost = 3 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SENSITIVITY_TO_MAGIC_KEY, Name = EnumAtoutFlaw.FLAW_SENSITIVITY_TO_MAGIC_NAME, Description = EnumAtoutFlaw.FLAW_SENSITIVITY_TO_MAGIC_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_MATHUSALEM_THIRST_KEY, Name = EnumAtoutFlaw.FLAW_MATHUSALEM_THIRST_NAME, Description = EnumAtoutFlaw.FLAW_MATHUSALEM_THIRST_DESCRIPTION, Cost = 4 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_INNOCENCE_THIRST_KEY, Name = EnumAtoutFlaw.FLAW_INNOCENCE_THIRST_NAME, Description = EnumAtoutFlaw.FLAW_INNOCENCE_THIRST_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DARK_FATE_KEY, Name = EnumAtoutFlaw.FLAW_DARK_FATE_NAME, Description = EnumAtoutFlaw.FLAW_DARK_FATE_DESCRIPTION, Cost = 5 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DARK_SECRET_KEY, Name = EnumAtoutFlaw.FLAW_DARK_SECRET_NAME, Description = EnumAtoutFlaw.FLAW_DARK_SECRET_DESCRIPTION, Cost = 1 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DEEP_SLEEP_KEY, Name = EnumAtoutFlaw.FLAW_DEEP_SLEEP_NAME, Description = EnumAtoutFlaw.FLAW_DEEP_SLEEP_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_MILK_KEY, Name = EnumAtoutFlaw.FLAW_MILK_NAME, Description = EnumAtoutFlaw.FLAW_MILK_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_DEATH_SIGHT_KEY, Name = EnumAtoutFlaw.FLAW_DEATH_SIGHT_NAME, Description = EnumAtoutFlaw.FLAW_DEATH_SIGHT_DESCRIPTION, Cost = 2 },
                    new Flaw { FlawKey = EnumAtoutFlaw.FLAW_SILVER_WEAKNESS_KEY, Name = EnumAtoutFlaw.FLAW_SILVER_WEAKNESS_NAME, Description = EnumAtoutFlaw.FLAW_SILVER_WEAKNESS_DESCRIPTION, Cost = 2 }
                );

                context.SaveChanges();
            }
        }

        public static void SeedAffiliate(DataContext context)
        {
            if (!context.Affiliates.Any())
            {
                new List<Affiliate> {
                    new Affiliate{AffiliateKey = EnumAffiliate.CAMARILLA, Description = EnumAffiliate.CAMARILLA_DESCRIPTION, Logo = "https://res.cloudinary.com/dbf5hrvz3/image/upload/v1592556671/SymbolCamarillaV5_qawmxh.png"},
                    new Affiliate{AffiliateKey = EnumAffiliate.SABBAT, Description = EnumAffiliate.SABBAT_DESCRIPTION, Logo = "https://res.cloudinary.com/dbf5hrvz3/image/upload/v1592557249/LogoSectSabbat_dnx2uy.png" },
                    new Affiliate{AffiliateKey = EnumAffiliate.ANARCH, Description = EnumAffiliate.ANARCH_DESCRIPTION, Logo = "https://res.cloudinary.com/dbf5hrvz3/image/upload/v1592558099/Anarch_sxwssw.png" },
                    new Affiliate{AffiliateKey = EnumAffiliate.INDEPENDENT_ALLIANCE, Description = EnumAffiliate.INDEPENDENT_ALLIANCE_DESCRIPTION },
                    new Affiliate{AffiliateKey = EnumAffiliate.INDEPENDENT_CLAN, Description = EnumAffiliate.INDEPENDENT_CLAN_DESCRIPTION }
                }.ForEach(a =>
                {
                    context.Affiliates.Add(a);
                });

                context.SaveChanges();
            }
        }

        public static void SeedFocus(DataContext context)
        {
            if (!context.Focus.Any())
            {
                new List<Focus>
                {
                    /*0*/ new Focus{FocusKey = EnumFocus.PHYSICAL_STRENGTH_KEY, FocusName = EnumFocus.PHYSICAL_STRENGTH_NAME, Description = EnumFocus.PHYSICAL_STRENGTH_DESCRIPTION },
                    /*1*/ new Focus{FocusKey = EnumFocus.PHYSICAL_DEXTERITY_KEY, FocusName = EnumFocus.PHYSICAL_DEXTERITY_NAME, Description = EnumFocus.PHYSICAL_DEXTERITY_DESCRIPTION },
                    /*2*/ new Focus{FocusKey = EnumFocus.PHYSICAL_STAMINA_KEY, FocusName = EnumFocus.PHYSICAL_STAMINA_NAME, Description = EnumFocus.PHYSICAL_STAMINA_DESCRIPTION },
                    /*3*/ new Focus{FocusKey = EnumFocus.SOCIAL_CHARISMA_KEY, FocusName = EnumFocus.SOCIAL_CHARISMA_NAME, Description = EnumFocus.SOCIAL_CHARISMA_DESCRIPTION },
                    /*4*/ new Focus{FocusKey = EnumFocus.SOCIAL_MANIPULATION_KEY, FocusName = EnumFocus.SOCIAL_MANIPULATION_NAME, Description = EnumFocus.SOCIAL_MANIPULATION_DESCRIPTION },
                    /*5*/ new Focus{FocusKey = EnumFocus.SOCIAL_APPEARANCE_KEY, FocusName = EnumFocus.SOCIAL_APPEARANCE_NAME, Description = EnumFocus.SOCIAL_APPEARANCE_DESCRIPTION },
                    /*6*/ new Focus{FocusKey = EnumFocus.MENTAL_PERCEPTION_KEY, FocusName = EnumFocus.MENTAL_PERCEPTION_NAME, Description = EnumFocus.MENTAL_PERCEPTION_DESCRIPTION },
                    /*7*/ new Focus{FocusKey = EnumFocus.MENTAL_INTELLIGENCE_KEY, FocusName = EnumFocus.MENTAL_INTELLIGENCE_NAME, Description = EnumFocus.MENTAL_INTELLIGENCE_DESCRIPTION },
                    /*8*/ new Focus{FocusKey = EnumFocus.MENTAL_WITZ_KEY, FocusName = EnumFocus.MENTAL_WITZ_NAME, Description = EnumFocus.MENTAL_WITZ_DESCRIPTION },
                }.ForEach(focus =>
                {
                    context.Focus.Add(focus);
                });

                context.SaveChanges();
            }
        }

        public static void SeedAttributs(DataContext context)
        {
            if (!context.Attributes.Any())
            {
                var physicalFocus = context.Focus.Where(x => x.FocusKey == EnumFocus.PHYSICAL_STRENGTH_KEY
                                                        || x.FocusKey == EnumFocus.PHYSICAL_DEXTERITY_KEY
                                                        || x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY).ToList();

                var socialFocus = context.Focus.Where(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY
                                                        || x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY
                                                        || x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY).ToList();

                var mentalFocus = context.Focus.Where(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY
                                                        || x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY
                                                        || x.FocusKey == EnumFocus.MENTAL_WITZ_KEY).ToList();

                new List<DefenseByNight.API.Data.Entities.Attribute>
                {
                    new DefenseByNight.API.Data.Entities.Attribute {AttributeKey = EnumAttribut.PHYSICAL_KEY, AttributeName = EnumAttribut.PHYSICAL_NAME, Description = EnumAttribut.PHYSICAL_DESCRIPTION, Focus = physicalFocus },
                    new DefenseByNight.API.Data.Entities.Attribute {AttributeKey = EnumAttribut.SOCIAL_KEY, AttributeName = EnumAttribut.SOCIAL_NAME, Description = EnumAttribut.SOCIAL_DESCRIPTION, Focus = socialFocus },
                    new DefenseByNight.API.Data.Entities.Attribute {AttributeKey = EnumAttribut.MENTAL_KEY, AttributeName = EnumAttribut.MENTAL_NAME, Description = EnumAttribut.MENTAL_DESCRIPTION, Focus = mentalFocus }
                }.ForEach(attribute =>
                {
                    context.Attributes.Add(attribute);
                });

                context.SaveChanges();
            }
        }

        public static void SeedDiscipline(DataContext context)
        {
            if (!context.Disciplines.Any())
            {
                AnimalismInitializer(context);
                AuspexInitializer(context);
                CelerityInitializer(context);
                ChimestryInitializer(context);
                DaimoinonInitializer(context);
                DementationInitializer(context);
                DominateInitializer(context);
                FortitudeInitializer(context);
                MelpomineeInitializer(context);
                MytherceriaInitializer(context);
                ObeahInitializer(context);
                ObfuscateInitializer(context);
                ObtenebrationInitializer(context);
                PotenceInitializer(context);
                PresenceInitializer(context);
                ProteanInitializer(context);
                QuietusInitializer(context);
                SerpentisInitializer(context);
                TemporisInitializer(context);
                ThanatosisInitializer(context);
                ValerenInitializer(context);
                VicissitudeInitializer(context);
                VisceratikaInitializer(context);

                SepulcherInitializer(context);
                BonesInitializer(context);
                AschesInitializer(context);
                MortisInitializer(context);
                BloodInitializer(context);
                CorruptionInitializer(context);
                ConjurationInitializer(context);
                ElementsInitializer(context);
                FireInitializer(context);
                SpiritInitializer(context);
                TechnologyInitializer(context);
            }
        }

        public static void SeedAtout(DataContext context)
        {
            if (!context.Atouts.Any())
            {
                var atouts = new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_AMBIDEXTROUS_KEY, Name = EnumAtoutFlaw.ATOUT_AMBIDEXTROUS_NAME, Description = EnumAtoutFlaw.ATOUT_AMBIDEXTROUS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_EEL_KEY, Name = EnumAtoutFlaw.ATOUT_EEL_NAME, Description = EnumAtoutFlaw.ATOUT_EEL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ABLE_IN_COMPETENCE_KEY, Name = EnumAtoutFlaw.ATOUT_ABLE_IN_COMPETENCE_NAME, Description = EnumAtoutFlaw.ATOUT_ABLE_IN_COMPETENCE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ARCANE_KEY, Name = EnumAtoutFlaw.ATOUT_ARCANE_NAME, Description = EnumAtoutFlaw.ATOUT_ARCANE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BLASE_KEY, Name = EnumAtoutFlaw.ATOUT_BLASE_NAME, Description = EnumAtoutFlaw.ATOUT_BLASE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_DARE_DEVIL_KEY, Name = EnumAtoutFlaw.ATOUT_DARE_DEVIL_NAME, Description = EnumAtoutFlaw.ATOUT_DARE_DEVIL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_PROPHECY_KEY, Name = EnumAtoutFlaw.ATOUT_PROPHECY_NAME, Description = EnumAtoutFlaw.ATOUT_PROPHECY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LUCKY_KEY, Name = EnumAtoutFlaw.ATOUT_LUCKY_NAME, Description = EnumAtoutFlaw.ATOUT_LUCKY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_FARSEEING_KEY, Name = EnumAtoutFlaw.ATOUT_FARSEEING_NAME, Description = EnumAtoutFlaw.ATOUT_FARSEEING_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_HONOR_KEY, Name = EnumAtoutFlaw.ATOUT_HONOR_NAME, Description = EnumAtoutFlaw.ATOUT_HONOR_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_DIGESTION_KEY, Name = EnumAtoutFlaw.ATOUT_DIGESTION_NAME, Description = EnumAtoutFlaw.ATOUT_DIGESTION_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_COMMON_DISCIPLINE_KEY, Name = EnumAtoutFlaw.ATOUT_COMMON_DISCIPLINE_NAME, Description = EnumAtoutFlaw.ATOUT_COMMON_DISCIPLINE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 4},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LANGUAGE_GIFT_KEY, Name = EnumAtoutFlaw.ATOUT_LANGUAGE_GIFT_NAME, Description = EnumAtoutFlaw.ATOUT_LANGUAGE_GIFT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SCHOLAR_KEY, Name = EnumAtoutFlaw.ATOUT_SCHOLAR_NAME, Description = EnumAtoutFlaw.ATOUT_SCHOLAR_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_NECROMANCY_KEY, Name = EnumAtoutFlaw.ATOUT_NECROMANCY_NAME, Description = EnumAtoutFlaw.ATOUT_NECROMANCY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 5},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_THAUMATURGY_KEY, Name = EnumAtoutFlaw.ATOUT_THAUMATURGY_NAME, Description = EnumAtoutFlaw.ATOUT_THAUMATURGY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 4},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_INALIENABLE_KEY, Name = EnumAtoutFlaw.ATOUT_INALIENABLE_NAME, Description = EnumAtoutFlaw.ATOUT_INALIENABLE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 4},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_INFLEXIBLE_KEY, Name = EnumAtoutFlaw.ATOUT_INFLEXIBLE_NAME, Description = EnumAtoutFlaw.ATOUT_INFLEXIBLE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 4},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_PERSONNAL_MASCARADE_KEY, Name = EnumAtoutFlaw.ATOUT_PERSONNAL_MASCARADE_NAME, Description = EnumAtoutFlaw.ATOUT_PERSONNAL_MASCARADE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MEDIUM_KEY, Name = EnumAtoutFlaw.ATOUT_MEDIUM_NAME, Description = EnumAtoutFlaw.ATOUT_MEDIUM_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_INFERNAL_POWER_KEY, Name = EnumAtoutFlaw.ATOUT_INFERNAL_POWER_NAME, Description = EnumAtoutFlaw.ATOUT_INFERNAL_POWER_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_PRECOCIOUS_KEY, Name = EnumAtoutFlaw.ATOUT_PRECOCIOUS_NAME, Description = EnumAtoutFlaw.ATOUT_PRECOCIOUS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_REPUTATION_KEY, Name = EnumAtoutFlaw.ATOUT_REPUTATION_NAME, Description = EnumAtoutFlaw.ATOUT_REPUTATION_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 2},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MAGIC_RESIST_KEY, Name = EnumAtoutFlaw.ATOUT_MAGIC_RESIST_NAME, Description = EnumAtoutFlaw.ATOUT_MAGIC_RESIST_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ROBUST_KEY, Name = EnumAtoutFlaw.ATOUT_ROBUST_NAME, Description = EnumAtoutFlaw.ATOUT_ROBUST_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_COOL_KEY, Name = EnumAtoutFlaw.ATOUT_COOL_NAME, Description = EnumAtoutFlaw.ATOUT_COOL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SENSE_KEY, Name = EnumAtoutFlaw.ATOUT_SENSE_NAME, Description = EnumAtoutFlaw.ATOUT_SENSE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LIGHT_SLEEP_KEY, Name = EnumAtoutFlaw.ATOUT_LIGHT_SLEEP_NAME, Description = EnumAtoutFlaw.ATOUT_LIGHT_SLEEP_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LIFE_BREATH_KEY, Name = EnumAtoutFlaw.ATOUT_LIFE_BREATH_NAME, Description = EnumAtoutFlaw.ATOUT_LIFE_BREATH_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 1},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GOLCONDE_PATH_KEY, Name = EnumAtoutFlaw.ATOUT_GOLCONDE_PATH_NAME, Description = EnumAtoutFlaw.ATOUT_GOLCONDE_PATH_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 5},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_JACK_OF_ALL_TRADE_KEY, Name = EnumAtoutFlaw.ATOUT_JACK_OF_ALL_TRADE_NAME, Description = EnumAtoutFlaw.ATOUT_JACK_OF_ALL_TRADE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_VITALITY_KEY, Name = EnumAtoutFlaw.ATOUT_VITALITY_NAME, Description = EnumAtoutFlaw.ATOUT_VITALITY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_IRON_WILL_KEY, Name = EnumAtoutFlaw.ATOUT_IRON_WILL_NAME, Description = EnumAtoutFlaw.ATOUT_IRON_WILL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.General, Cost = 3},
                };

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_UNCOMMUN_CLAN_KEY, Name = EnumAtoutFlaw.ATOUT_UNCOMMUN_CLAN_NAME, Description = EnumAtoutFlaw.ATOUT_UNCOMMUN_CLAN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Rarity, Cost = 2 },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_RARE_CLAN_KEY, Name = EnumAtoutFlaw.ATOUT_RARE_CLAN_NAME, Description = EnumAtoutFlaw.ATOUT_RARE_CLAN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Rarity, Cost = 4 },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_RESTRAINED_CLAN_KEY, Name = EnumAtoutFlaw.ATOUT_RESTRAINED_CLAN_NAME, Description = EnumAtoutFlaw.ATOUT_RESTRAINED_CLAN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Rarity, Cost = 6 },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ASSAMITE_SURPRISE_KEY, Name = EnumAtoutFlaw.ATOUT_ASSAMITE_SURPRISE_NAME, Description = EnumAtoutFlaw.ATOUT_ASSAMITE_SURPRISE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumAssamite.CLAN_ASSAMITE)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ASSAMITE_VIZIR_KEY, Name = EnumAtoutFlaw.ATOUT_ASSAMITE_VIZIR_NAME, Description = EnumAtoutFlaw.ATOUT_ASSAMITE_VIZIR_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumAssamite.CLAN_ASSAMITE)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ASSAMITE_STEAL_KEY, Name = EnumAtoutFlaw.ATOUT_ASSAMITE_STEAL_NAME, Description = EnumAtoutFlaw.ATOUT_ASSAMITE_STEAL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumAssamite.CLAN_ASSAMITE)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_ASSAMITE_WARLOCK_KEY, Name = EnumAtoutFlaw.ATOUT_ASSAMITE_WARLOCK_NAME, Description = EnumAtoutFlaw.ATOUT_ASSAMITE_WARLOCK_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumAssamite.CLAN_ASSAMITE)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BRUJAH_BROTHERHOOD_KEY, Name = EnumAtoutFlaw.ATOUT_BRUJAH_BROTHERHOOD_NAME, Description = EnumAtoutFlaw.ATOUT_BRUJAH_BROTHERHOOD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBrujah.CLAN_BRUJAH)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BRUJAH_ANGER_KEY, Name = EnumAtoutFlaw.ATOUT_BRUJAH_ANGER_NAME, Description = EnumAtoutFlaw.ATOUT_BRUJAH_ANGER_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBrujah.CLAN_BRUJAH)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BRUJAH_ALECTO_KEY, Name = EnumAtoutFlaw.ATOUT_BRUJAH_ALECTO_NAME, Description = EnumAtoutFlaw.ATOUT_BRUJAH_ALECTO_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBrujah.CLAN_BRUJAH)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BRUJAH_TRUE_KEY, Name = EnumAtoutFlaw.ATOUT_BRUJAH_TRUE_NAME, Description = EnumAtoutFlaw.ATOUT_BRUJAH_TRUE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBrujah.CLAN_BRUJAH)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SET_PERSONNAL_KULT_KEY, Name = EnumAtoutFlaw.ATOUT_SET_PERSONNAL_KULT_NAME, Description = EnumAtoutFlaw.ATOUT_SET_PERSONNAL_KULT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSet.CLAN_SET)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SET_VIPERS_KEY, Name = EnumAtoutFlaw.ATOUT_SET_VIPERS_NAME, Description = EnumAtoutFlaw.ATOUT_SET_VIPERS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSet.CLAN_SET)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SET_TLACIQUE_KEY, Name = EnumAtoutFlaw.ATOUT_SET_TLACIQUE_NAME, Description = EnumAtoutFlaw.ATOUT_SET_TLACIQUE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSet.CLAN_SET)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SET_BLOOD_KEY, Name = EnumAtoutFlaw.ATOUT_SET_BLOOD_NAME, Description = EnumAtoutFlaw.ATOUT_SET_BLOOD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSet.CLAN_SET)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SET_WITCHCRAFT_KEY, Name = EnumAtoutFlaw.ATOUT_SET_WITCHCRAFT_NAME, Description = EnumAtoutFlaw.ATOUT_SET_WITCHCRAFT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSet.CLAN_SET)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GANGREL_BLOOD_KEY, Name = EnumAtoutFlaw.ATOUT_GANGREL_BLOOD_NAME, Description = EnumAtoutFlaw.ATOUT_GANGREL_BLOOD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGangrel.CLAN_GANGREL)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GANGREL_COYOTE_KEY, Name = EnumAtoutFlaw.ATOUT_GANGREL_COYOTE_NAME, Description = EnumAtoutFlaw.ATOUT_GANGREL_COYOTE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGangrel.CLAN_GANGREL)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GANGREL_NOIAD_KEY, Name = EnumAtoutFlaw.ATOUT_GANGREL_NOIAD_NAME, Description = EnumAtoutFlaw.ATOUT_GANGREL_NOIAD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGangrel.CLAN_GANGREL)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GANGREL_BEAST_ANGER_KEY, Name = EnumAtoutFlaw.ATOUT_GANGREL_BEAST_ANGER_NAME, Description = EnumAtoutFlaw.ATOUT_GANGREL_BEAST_ANGER_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGangrel.CLAN_GANGREL)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GANGREL_AHRIMANES_KEY, Name = EnumAtoutFlaw.ATOUT_GANGREL_AHRIMANES_NAME, Description = EnumAtoutFlaw.ATOUT_GANGREL_AHRIMANES_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGangrel.CLAN_GANGREL)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GIOVANNI_NECROMANCY_KEY, Name = EnumAtoutFlaw.ATOUT_GIOVANNI_NECROMANCY_NAME, Description = EnumAtoutFlaw.ATOUT_GIOVANNI_NECROMANCY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGiovanni.CLAN_GIOVANNI)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GIOVANNI_BIG_ARMS_KEY, Name = EnumAtoutFlaw.ATOUT_GIOVANNI_BIG_ARMS_NAME, Description = EnumAtoutFlaw.ATOUT_GIOVANNI_BIG_ARMS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGiovanni.CLAN_GIOVANNI)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GIOVANNI_GHOST_KEY, Name = EnumAtoutFlaw.ATOUT_GIOVANNI_GHOST_NAME, Description = EnumAtoutFlaw.ATOUT_GIOVANNI_GHOST_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGiovanni.CLAN_GIOVANNI)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GIOVANNI_PREMASCINE_KEY, Name = EnumAtoutFlaw.ATOUT_GIOVANNI_PREMASCINE_NAME, Description = EnumAtoutFlaw.ATOUT_GIOVANNI_PREMASCINE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGiovanni.CLAN_GIOVANNI)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LASOMBRA_ANGEL_FACE_KEY, Name = EnumAtoutFlaw.ATOUT_LASOMBRA_ANGEL_FACE_NAME, Description = EnumAtoutFlaw.ATOUT_LASOMBRA_ANGEL_FACE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumLasombra.CLAN_LASOMBRA) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LASOMBRA_BORN_IN_SHADOWS_KEY, Name = EnumAtoutFlaw.ATOUT_LASOMBRA_BORN_IN_SHADOWS_NAME, Description = EnumAtoutFlaw.ATOUT_LASOMBRA_BORN_IN_SHADOWS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumLasombra.CLAN_LASOMBRA) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LASOMBRA_ABYSS_KEY, Name = EnumAtoutFlaw.ATOUT_LASOMBRA_ABYSS_NAME, Description = EnumAtoutFlaw.ATOUT_LASOMBRA_ABYSS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumLasombra.CLAN_LASOMBRA) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_LASOMBRA_KIASYDE_KEY, Name = EnumAtoutFlaw.ATOUT_LASOMBRA_KIASYDE_NAME, Description = EnumAtoutFlaw.ATOUT_LASOMBRA_KIASYDE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumLasombra.CLAN_LASOMBRA)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MALKAVIEN_EXTENDED_AWARENESS_KEY, Name = EnumAtoutFlaw.ATOUT_MALKAVIEN_EXTENDED_AWARENESS_NAME, Description = EnumAtoutFlaw.ATOUT_MALKAVIEN_EXTENDED_AWARENESS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumMalkavian.CLAN_MALKAVIAN) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MALKAVIEN_ANANKE_KEY, Name = EnumAtoutFlaw.ATOUT_MALKAVIEN_ANANKE_NAME, Description = EnumAtoutFlaw.ATOUT_MALKAVIEN_ANANKE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumMalkavian.CLAN_MALKAVIAN) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MALKAVIEN_MOON_KNIGHT_KEY, Name = EnumAtoutFlaw.ATOUT_MALKAVIEN_MOON_KNIGHT_NAME, Description = EnumAtoutFlaw.ATOUT_MALKAVIEN_MOON_KNIGHT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumMalkavian.CLAN_MALKAVIAN) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MALKAVIEN_MAZE_MIND_KEY, Name = EnumAtoutFlaw.ATOUT_MALKAVIEN_MAZE_MIND_NAME, Description = EnumAtoutFlaw.ATOUT_MALKAVIEN_MAZE_MIND_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumMalkavian.CLAN_MALKAVIAN) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_MALKAVIEN_SOPHISTICATED_KEY, Name = EnumAtoutFlaw.ATOUT_MALKAVIEN_SOPHISTICATED_NAME, Description = EnumAtoutFlaw.ATOUT_MALKAVIEN_SOPHISTICATED_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumMalkavian.CLAN_MALKAVIAN)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_NOSFERATU_BLIND_EYE_KEY, Name = EnumAtoutFlaw.ATOUT_NOSFERATU_BLIND_EYE_NAME, Description = EnumAtoutFlaw.ATOUT_NOSFERATU_BLIND_EYE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumNosferatu.CLAN_NOSFERATU) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_NOSFERATU_HIDDEN_ATOUT_KEY, Name = EnumAtoutFlaw.ATOUT_NOSFERATU_HIDDEN_ATOUT_NAME, Description = EnumAtoutFlaw.ATOUT_NOSFERATU_HIDDEN_ATOUT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumNosferatu.CLAN_NOSFERATU) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_NOSFERATU_MALLEABLE_BLOOD_KEY, Name = EnumAtoutFlaw.ATOUT_NOSFERATU_MALLEABLE_BLOOD_NAME, Description = EnumAtoutFlaw.ATOUT_NOSFERATU_MALLEABLE_BLOOD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumNosferatu.CLAN_NOSFERATU) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_NOSFERATU_UNATURAL_ADAPTATION_KEY, Name = EnumAtoutFlaw.ATOUT_NOSFERATU_UNATURAL_ADAPTATION_NAME, Description = EnumAtoutFlaw.ATOUT_NOSFERATU_UNATURAL_ADAPTATION_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumNosferatu.CLAN_NOSFERATU) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TOREADOR_ARTIST_BLESS_KEY, Name = EnumAtoutFlaw.ATOUT_TOREADOR_ARTIST_BLESS_NAME, Description = EnumAtoutFlaw.ATOUT_TOREADOR_ARTIST_BLESS_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumToreador.CLAN_TOREADOR) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TOREADOR_ISHTARRI_KEY, Name = EnumAtoutFlaw.ATOUT_TOREADOR_ISHTARRI_NAME, Description = EnumAtoutFlaw.ATOUT_TOREADOR_ISHTARRI_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumToreador.CLAN_TOREADOR) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TOREADOR_VOLGIRRE_KEY, Name = EnumAtoutFlaw.ATOUT_TOREADOR_VOLGIRRE_NAME, Description = EnumAtoutFlaw.ATOUT_TOREADOR_VOLGIRRE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumToreador.CLAN_TOREADOR) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TOREADOR_ABSENT_INFLUENCE_KEY, Name = EnumAtoutFlaw.ATOUT_TOREADOR_ABSENT_INFLUENCE_NAME, Description = EnumAtoutFlaw.ATOUT_TOREADOR_ABSENT_INFLUENCE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumToreador.CLAN_TOREADOR)},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TOREADOR_GRACE_OF_THE_DANCER_KEY, Name = EnumAtoutFlaw.ATOUT_TOREADOR_GRACE_OF_THE_DANCER_NAME, Description = EnumAtoutFlaw.ATOUT_TOREADOR_GRACE_OF_THE_DANCER_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumToreador.CLAN_TOREADOR)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TREMERE_EXPERTISE_KEY, Name = EnumAtoutFlaw.ATOUT_TREMERE_EXPERTISE_NAME, Description = EnumAtoutFlaw.ATOUT_TREMERE_EXPERTISE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTremere.CLAN_TREMERE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TREMERE_TELYAV_KEY, Name = EnumAtoutFlaw.ATOUT_TREMERE_TELYAV_NAME, Description = EnumAtoutFlaw.ATOUT_TREMERE_TELYAV_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTremere.CLAN_TREMERE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TREMERE_TALISMAN_KEY, Name = EnumAtoutFlaw.ATOUT_TREMERE_TALISMAN_NAME, Description = EnumAtoutFlaw.ATOUT_TREMERE_TALISMAN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTremere.CLAN_TREMERE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TREMERE_COUNTER_MAGIE_KEY, Name = EnumAtoutFlaw.ATOUT_TREMERE_COUNTER_MAGIE_NAME, Description = EnumAtoutFlaw.ATOUT_TREMERE_COUNTER_MAGIE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4,Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTremere.CLAN_TREMERE)},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TZIMISCE_TZIMISCE_BLOOD_KEY, Name = EnumAtoutFlaw.ATOUT_TZIMISCE_TZIMISCE_BLOOD_NAME, Description = EnumAtoutFlaw.ATOUT_TZIMISCE_TZIMISCE_BLOOD_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTzimisce.CLAN_TZIMISCE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TZIMISCE_SZLACHTA_KEY, Name = EnumAtoutFlaw.ATOUT_TZIMISCE_SZLACHTA_NAME, Description = EnumAtoutFlaw.ATOUT_TZIMISCE_SZLACHTA_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTzimisce.CLAN_TZIMISCE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TZIMISCE_CARPATIQUE_KEY, Name = EnumAtoutFlaw.ATOUT_TZIMISCE_CARPATIQUE_NAME, Description = EnumAtoutFlaw.ATOUT_TZIMISCE_CARPATIQUE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTzimisce.CLAN_TZIMISCE) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_TZIMISCE_KOLDUN_KEY, Name = EnumAtoutFlaw.ATOUT_TZIMISCE_KOLDUN_NAME, Description = EnumAtoutFlaw.ATOUT_TZIMISCE_KOLDUN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumTzimisce.CLAN_TZIMISCE) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_VENTRUE_AURA_OF_COMMAND_KEY, Name = EnumAtoutFlaw.ATOUT_VENTRUE_AURA_OF_COMMAND_NAME, Description = EnumAtoutFlaw.ATOUT_VENTRUE_AURA_OF_COMMAND_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumVentrue.CLAN_VENTRUE ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_VENTRUE_CROISE_KEY, Name = EnumAtoutFlaw.ATOUT_VENTRUE_CROISE_NAME, Description = EnumAtoutFlaw.ATOUT_VENTRUE_CROISE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumVentrue.CLAN_VENTRUE ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_VENTRUE_PARANGON_KEY, Name = EnumAtoutFlaw.ATOUT_VENTRUE_PARANGON_NAME, Description = EnumAtoutFlaw.ATOUT_VENTRUE_PARANGON_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumVentrue.CLAN_VENTRUE ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_VENTRUE_ROYAL_KEY, Name = EnumAtoutFlaw.ATOUT_VENTRUE_ROYAL_NAME, Description = EnumAtoutFlaw.ATOUT_VENTRUE_ROYAL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumVentrue.CLAN_VENTRUE ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAITIFF_PROPICE_KEY, Name = EnumAtoutFlaw.ATOUT_CAITIFF_PROPICE_NAME, Description = EnumAtoutFlaw.ATOUT_CAITIFF_PROPICE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCaitiff.CLAN_CAITIFF ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAITIFF_SANG_ECLIPSE_KEY, Name = EnumAtoutFlaw.ATOUT_CAITIFF_SANG_ECLIPSE_NAME, Description = EnumAtoutFlaw.ATOUT_CAITIFF_SANG_ECLIPSE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCaitiff.CLAN_CAITIFF ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAITIFF_VESTIGES_GRANDEUR_KEY, Name = EnumAtoutFlaw.ATOUT_CAITIFF_VESTIGES_GRANDEUR_NAME, Description = EnumAtoutFlaw.ATOUT_CAITIFF_VESTIGES_GRANDEUR_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCaitiff.CLAN_CAITIFF ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BAALI_INFERNAL_LEGACY_KEY, Name = EnumAtoutFlaw.ATOUT_BAALI_INFERNAL_LEGACY_NAME, Description = EnumAtoutFlaw.ATOUT_BAALI_INFERNAL_LEGACY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBaali.CLAN_BAALI ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_BAALI_ANGELLIS_ATER_KEY, Name = EnumAtoutFlaw.ATOUT_BAALI_ANGELLIS_ATER_NAME, Description = EnumAtoutFlaw.ATOUT_BAALI_ANGELLIS_ATER_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumBaali.CLAN_BAALI ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_NECROMANTIC_INSTINCT_KEY, Name = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_NECROMANTIC_INSTINCT_NAME, Description = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_NECROMANTIC_INSTINCT_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCappadocian.CLAN_CAPPADOCIAN ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_SAMEDI_KEY, Name = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_SAMEDI_NAME, Description = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_SAMEDI_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCappadocian.CLAN_CAPPADOCIAN ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_SAIL_KEY, Name = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_SAIL_NAME, Description = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_SAIL_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCappadocian.CLAN_CAPPADOCIAN ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_LAMIA_KEY, Name = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_LAMIA_NAME, Description = EnumAtoutFlaw.ATOUT_CAPPADOCIAN_BLOODLINE_LAMIA_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 4, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCappadocian.CLAN_CAPPADOCIAN ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_RAVNOS_DAYDREAM_KEY, Name = EnumAtoutFlaw.ATOUT_RAVNOS_DAYDREAM_NAME, Description = EnumAtoutFlaw.ATOUT_RAVNOS_DAYDREAM_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumRavnos.CLAN_RAVNOS ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_RAVNOS_BLOODLINE_BRAHMAN_KEY, Name = EnumAtoutFlaw.ATOUT_RAVNOS_BLOODLINE_BRAHMAN_NAME, Description = EnumAtoutFlaw.ATOUT_RAVNOS_BLOODLINE_BRAHMAN_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumRavnos.CLAN_RAVNOS )},
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_RAVNOS_KING_ESCAPE_KEY, Name = EnumAtoutFlaw.ATOUT_RAVNOS_KING_ESCAPE_NAME, Description = EnumAtoutFlaw.ATOUT_RAVNOS_KING_ESCAPE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumRavnos.CLAN_RAVNOS )},
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SALUBRI_JUST_FURY_KEY, Name = EnumAtoutFlaw.ATOUT_SALUBRI_JUST_FURY_NAME, Description = EnumAtoutFlaw.ATOUT_SALUBRI_JUST_FURY_DECRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSalubri.CLAN_SALUBRI ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SALUBRI_SPIRIT_ARMOR_KEY, Name = EnumAtoutFlaw.ATOUT_SALUBRI_SPIRIT_ARMOR_NAME, Description = EnumAtoutFlaw.ATOUT_SALUBRI_SPIRIT_ARMOR_DECRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 2, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSalubri.CLAN_SALUBRI )  },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_SALUBRI_BLOODLINE_HEALER_KEY, Name = EnumAtoutFlaw.ATOUT_SALUBRI_BLOODLINE_HEALER_NAME, Description = EnumAtoutFlaw.ATOUT_SALUBRI_BLOODLINE_HEALER_DECRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumSalubri.CLAN_SALUBRI ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CACOPHONY_SURNATURAL_ARIA_KEY, Name = EnumAtoutFlaw.ATOUT_CACOPHONY_SURNATURAL_ARIA_NAME, Description = EnumAtoutFlaw.ATOUT_CACOPHONY_SURNATURAL_ARIA_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCacophony.CLAN_CACOPHONY ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_CACOPHONY_CELESTIAL_OCTAVES_KEY, Name = EnumAtoutFlaw.ATOUT_CACOPHONY_CELESTIAL_OCTAVES_NAME, Description = EnumAtoutFlaw.ATOUT_CACOPHONY_CELESTIAL_OCTAVES_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumCacophony.CLAN_CACOPHONY ) },
                });

                atouts.AddRange(new List<Atout>
                {
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GARGOYLE_FLY_KEY, Name = EnumAtoutFlaw.ATOUT_GARGOYLE_FLY_NAME, Description = EnumAtoutFlaw.ATOUT_GARGOYLE_FLY_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 1, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGargoyle.CLAN_GARGOYLE ) },
                    new Atout{ AtoutKey = EnumAtoutFlaw.ATOUT_GARGOYLE_DARK_STATUE_KEY, Name = EnumAtoutFlaw.ATOUT_GARGOYLE_DARK_STATUE_NAME, Description = EnumAtoutFlaw.ATOUT_GARGOYLE_DARK_STATUE_DESCRIPTION, Type = EnumAtoutFlaw.TypeAtout.Clan, Cost = 3, Clan = context.Clans.FirstOrDefault(c => c.ClanKey == EnumGargoyle.CLAN_GARGOYLE ) },
                });


                atouts.ForEach(a =>
                    {
                        context.Atouts.Add(a);
                    });

                context.SaveChanges();
            }
        }

        public static void SeedClans(DataContext context)
        {
            if (!context.Clans.Any())
            {
                context.Clans.Add(new Clan
                {
                    ClanKey = EnumAssamite.CLAN_ASSAMITE,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumCelerity.CELERITY_KEY || disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY || disci.DisciplineKey == EnumQuietus.QUIESTUS_KEY select disci).ToList(),
                    History = EnumAssamite.CLAN_ASSAMITE_HISTORY,
                    Name = EnumAssamite.CLAN_ASSAMITE_NAME,
                    Organisation = EnumAssamite.CLAN_ASSAMITE_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumAssamite.CLAN_ASSAMITE_SURNAME,
                    Weakness = EnumAssamite.CLAN_ASSAMITE_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumBrujah.CLAN_BRUJAH,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumCelerity.CELERITY_KEY || disci.DisciplineKey == EnumPotence.POTENCE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY select disci).ToList(),
                    History = EnumBrujah.CLAN_BRUJAH_HISTORY,
                    Name = EnumBrujah.CLAN_BRUJAH_NAME,
                    Organisation = EnumBrujah.CLAN_BRUJAH_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumBrujah.CLAN_BRUJAH_SURNAME,
                    Weakness = EnumBrujah.CLAN_BRUJAH_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumSet.CLAN_SET,
                    Affiliate = EnumAffiliate.INDEPENDENT_ALLIANCE,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY || disci.DisciplineKey == EnumSerpentis.SERPENTIS_KEY select disci).ToList(),
                    History = EnumSet.CLAN_SET_HISTORY,
                    Name = EnumSet.CLAN_SET_NAME,
                    Organisation = EnumSet.CLAN_SET_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumSet.CLAN_SET_SURNAME,
                    Weakness = EnumSet.CLAN_SET_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumGangrel.CLAN_GANGREL,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAnimalism.ANIMALISM_KEY || disci.DisciplineKey == EnumProtean.PROTEAN_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY select disci).ToList(),
                    History = EnumGangrel.CLAN_GANGREL_HISTORY,
                    Name = EnumGangrel.CLAN_GANGREL_NAME,
                    Organisation = EnumGangrel.CLAN_GANGREL_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumGangrel.CLAN_GANGREL_SURNAME,
                    Weakness = EnumGangrel.CLAN_GANGREL_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumGiovanni.CLAN_GIOVANNI,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumDominate.DOMINATE_KEY || disci.DisciplineKey == EnumPotence.POTENCE_KEY || disci.DisciplineKey == EnumSepulcher.SEPULCHER_KEY select disci).ToList(),
                    History = EnumGiovanni.CLAN_GIOVANNI_HISTORY,
                    Name = EnumGiovanni.CLAN_GIOVANNI_NAME,
                    Organisation = EnumGiovanni.CLAN_GIOVANNI_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumGiovanni.CLAN_GIOVANNI_SURNAME,
                    Weakness = EnumGiovanni.CLAN_GIOVANNI_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumLasombra.CLAN_LASOMBRA,
                    Affiliate = EnumAffiliate.SABBAT,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumDominate.DOMINATE_KEY || disci.DisciplineKey == EnumPotence.POTENCE_KEY || disci.DisciplineKey == EnumObtenebration.OBTENEBRATION_KEY select disci).ToList(),
                    History = EnumLasombra.CLAN_LASOMBRA_HISTORY,
                    Name = EnumLasombra.CLAN_LASOMBRA_NAME,
                    Organisation = EnumLasombra.CLAN_LASOMBRA_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumLasombra.CLAN_LASOMBRA_SURNAME,
                    Weakness = EnumLasombra.CLAN_LASOMBRA_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumMalkavian.CLAN_MALKAVIAN,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumDementation.DEMENTATION_KEY || disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY select disci).ToList(),
                    History = EnumMalkavian.CLAN_MALKAVIAN_HISTORY,
                    Name = EnumMalkavian.CLAN_MALKAVIAN_NAME,
                    Organisation = EnumMalkavian.CLAN_MALKAVIAN_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumMalkavian.CLAN_MALKAVIAN_SURNAME,
                    Weakness = EnumMalkavian.CLAN_MALKAVIAN_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumNosferatu.CLAN_NOSFERATU,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAnimalism.ANIMALISM_KEY || disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY || disci.DisciplineKey == EnumPotence.POTENCE_KEY select disci).ToList(),
                    History = EnumNosferatu.CLAN_NOSFERATU_HISTORY,
                    Name = EnumNosferatu.CLAN_NOSFERATU_NAME,
                    Organisation = EnumNosferatu.CLAN_NOSFERATU_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumNosferatu.CLAN_NOSFERATU_SURNAME,
                    Weakness = EnumNosferatu.CLAN_NOSFERATU_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumToreador.CLAN_TOREADOR,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumCelerity.CELERITY_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY select disci).ToList(),
                    History = EnumToreador.CLAN_TOREADOR_HISTORY,
                    Name = EnumToreador.CLAN_TOREADOR_NAME,
                    Organisation = EnumToreador.CLAN_TOREADOR_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumToreador.CLAN_TOREADOR_SURNAME,
                    Weakness = EnumToreador.CLAN_TOREADOR_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumTremere.CLAN_TREMERE,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumDominate.DOMINATE_KEY || disci.DisciplineKey == EnumBlood.BLOOD_KEY select disci).ToList(),
                    History = EnumTremere.CLAN_TREMERE_HISTORY,
                    Name = EnumTremere.CLAN_TREMERE_NAME,
                    Organisation = EnumTremere.CLAN_TREMERE_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumTremere.CLAN_TREMERE_SURNAME,
                    Weakness = EnumTremere.CLAN_TREMERE_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumTzimisce.CLAN_TZIMISCE,
                    Affiliate = EnumAffiliate.SABBAT,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAnimalism.ANIMALISM_KEY || disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumVicissitude.VICISSITUDE_KEY select disci).ToList(),
                    History = EnumTzimisce.CLAN_TZIMISCE_HISTORY,
                    Name = EnumTzimisce.CLAN_TZIMISCE_NAME,
                    Organisation = EnumTzimisce.CLAN_TZIMISCE_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumTzimisce.CLAN_TZIMISCE_SURNAME,
                    Weakness = EnumTzimisce.CLAN_TZIMISCE_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumVentrue.CLAN_VENTRUE,
                    Affiliate = EnumAffiliate.CAMARILLA,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumDominate.DOMINATE_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY select disci).ToList(),
                    History = EnumVentrue.CLAN_VENTRUE_HISTORY,
                    Name = EnumVentrue.CLAN_VENTRUE_NAME,
                    Organisation = EnumVentrue.CLAN_VENTRUE_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumVentrue.CLAN_VENTRUE_SURNAME,
                    Weakness = EnumVentrue.CLAN_VENTRUE_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumCaitiff.CLAN_CAITIFF,
                    Affiliate = EnumAffiliate.ALL_SECTS,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAnimalism.ANIMALISM_KEY || disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumCelerity.CELERITY_KEY || disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY || disci.DisciplineKey == EnumDominate.DOMINATE_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY || disci.DisciplineKey == EnumPotence.POTENCE_KEY select disci).ToList(),
                    History = EnumCaitiff.CLAN_CAITIFF_HISTORY,
                    Name = EnumCaitiff.CLAN_CAITIFF_NAME,
                    Organisation = EnumCaitiff.CLAN_CAITIFF_ORGANIZATION,
                    RarityClan = EnumRarityClan.Major,
                    Surname = EnumCaitiff.CLAN_CAITIFF_SURNAME,
                    Weakness = EnumCaitiff.CLAN_CAITIFF_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumBaali.CLAN_BAALI,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumDaimonion.DAIMOINON_KEY || disci.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY select disci).ToList(),
                    History = EnumBaali.CLAN_BAALI_HISTORY,
                    Name = EnumBaali.CLAN_BAALI_NAME,
                    Organisation = EnumBaali.CLAN_BAALI_ORGANIZATION,
                    RarityClan = EnumRarityClan.Minor,
                    Surname = EnumBaali.CLAN_BAALI_SURNAME,
                    Weakness = EnumBaali.CLAN_BAALI_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumCappadocian.CLAN_CAPPADOCIAN,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumMortis.MORTIS_KEY select disci).ToList(),
                    History = EnumCappadocian.CLAN_CAPPADOCIAN_HISTORY,
                    Name = EnumCappadocian.CLAN_CAPPADOCIAN_NAME,
                    Organisation = EnumCappadocian.CLAN_CAPPADOCIAN_ORGANIZATION,
                    RarityClan = EnumRarityClan.Minor,
                    Surname = EnumCappadocian.CLAN_CAPPADOCIAN_SURNAME,
                    Weakness = EnumCappadocian.CLAN_CAPPADOCIAN_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumRavnos.CLAN_RAVNOS,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAnimalism.ANIMALISM_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumChimerstry.CHIMERSTRY_KEY select disci).ToList(),
                    History = EnumRavnos.CLAN_RAVNOS_HISTORY,
                    Name = EnumRavnos.CLAN_RAVNOS_NAME,
                    Organisation = EnumRavnos.CLAN_RAVNOS_ORGANIZATION,
                    RarityClan = EnumRarityClan.Minor,
                    Surname = EnumRavnos.CLAN_RAVNOS_SURNAME,
                    Weakness = EnumRavnos.CLAN_RAVNOS_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumSalubri.CLAN_SALUBRI,
                    Affiliate = EnumAffiliate.SABBAT,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumAuspex.AUSPEX_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumValeren.VALEREN_KEY select disci).ToList(),
                    History = EnumSalubri.CLAN_SALUBRI_HISTORY,
                    Name = EnumSalubri.CLAN_SALUBRI_NAME,
                    Organisation = EnumSalubri.CLAN_SALUBRI_ORGANIZATION,
                    RarityClan = EnumRarityClan.Minor,
                    Surname = EnumSalubri.CLAN_SALUBRI_SURNAME,
                    Weakness = EnumSalubri.CLAN_SALUBRI_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumCacophony.CLAN_CACOPHONY,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumMelpominee.MELPOMINEE_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumPresence.PRESENCE_KEY select disci).ToList(),
                    History = EnumCacophony.CLAN_CACOPHONY_HISTORY,
                    Name = EnumCacophony.CLAN_CACOPHONY_NAME,
                    Organisation = EnumCacophony.CLAN_CACOPHONY_ORGANIZATION,
                    RarityClan = EnumRarityClan.Rare,
                    Surname = EnumCacophony.CLAN_CACOPHONY_SURNAME,
                    Weakness = EnumCacophony.CLAN_CACOPHONY_WEAKNESS
                });

                context.Clans.Add(new Clan
                {
                    ClanKey = EnumGargoyle.CLAN_GARGOYLE,
                    Affiliate = EnumAffiliate.INDEPENDENT_CLAN,
                    Disciplines = (from disci in context.Disciplines where disci.DisciplineKey == EnumPotence.POTENCE_KEY || disci.DisciplineKey == EnumFortitude.FORTITUDE_KEY || disci.DisciplineKey == EnumVisceratika.VISCERATIKA_KEY select disci).ToList(),
                    History = EnumGargoyle.CLAN_GARGOYLE_HISTORY,
                    Name = EnumGargoyle.CLAN_GARGOYLE_NAME,
                    Organisation = EnumGargoyle.CLAN_GARGOYLE_ORGANIZATION,
                    RarityClan = EnumRarityClan.Rare,
                    Surname = EnumGargoyle.CLAN_GARGOYLE_SURNAME,
                    Weakness = EnumGargoyle.CLAN_GARGOYLE_WEAKNESS
                });

            }
        }

        #region Disciplines
        private static void AnimalismInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumAnimalism.ANIMALISM_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumAnimalism.ANIMALISM_POWER_1_KEY, PowerName = EnumAnimalism.ANIMALISM_POWER_1_NAME, Description = EnumAnimalism.ANIMALISM_POWER_1_DESCRIPTION, System = EnumAnimalism.ANIMALISM_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumAnimalism.ANIMALISM_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAnimalism.ANIMALISM_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumAnimalism.ANIMALISM_POWER_2_KEY, PowerName = EnumAnimalism.ANIMALISM_POWER_2_NAME, Description = EnumAnimalism.ANIMALISM_POWER_2_DESCRIPTION, System = EnumAnimalism.ANIMALISM_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumAnimalism.ANIMALISM_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAnimalism.ANIMALISM_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumAnimalism.ANIMALISM_POWER_3_KEY, PowerName = EnumAnimalism.ANIMALISM_POWER_3_NAME, Description = EnumAnimalism.ANIMALISM_POWER_3_DESCRIPTION, System = EnumAnimalism.ANIMALISM_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAnimalism.ANIMALISM_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAnimalism.ANIMALISM_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumAnimalism.ANIMALISM_POWER_4_KEY, PowerName = EnumAnimalism.ANIMALISM_POWER_4_NAME, Description = EnumAnimalism.ANIMALISM_POWER_4_DESCRIPTION, System = EnumAnimalism.ANIMALISM_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAnimalism.ANIMALISM_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAnimalism.ANIMALISM_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumAnimalism.ANIMALISM_POWER_5_KEY, PowerName = EnumAnimalism.ANIMALISM_POWER_5_NAME, Description = EnumAnimalism.ANIMALISM_POWER_5_DESCRIPTION, System = EnumAnimalism.ANIMALISM_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAnimalism.ANIMALISM_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAnimalism.ANIMALISM_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumAnimalism.ANIMALISM_ELDER_POWER_1_KEY, PowerName = EnumAnimalism.ANIMALISM_ELDER_POWER_1_NAME, Description = EnumAnimalism.ANIMALISM_ELDER_POWER_1_DESCRIPTION, System = EnumAnimalism.ANIMALISM_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumAnimalism.ANIMALISM_ELDER_POWER_2_KEY, PowerName = EnumAnimalism.ANIMALISM_ELDER_POWER_2_NAME, Description = EnumAnimalism.ANIMALISM_ELDER_POWER_2_DESCRIPTION, System = EnumAnimalism.ANIMALISM_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumAnimalism.ANIMALISM_KEY,
                    DisciplineName = EnumAnimalism.ANIMALISM_NAME,
                    Description = EnumAnimalism.ANIMALISM_DESCRIPTION,
                    TestScore = EnumAnimalism.ANIMALISM_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void AuspexInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumAuspex.AUSPEX_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumAuspex.AUSPEX_POWER_1_KEY, PowerName = EnumAuspex.AUSPEX_POWER_1_NAME, Description = EnumAuspex.AUSPEX_POWER_1_DESCRIPTION, System = EnumAuspex.AUSPEX_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumAuspex.AUSPEX_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAuspex.AUSPEX_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumAuspex.AUSPEX_POWER_2_KEY, PowerName = EnumAuspex.AUSPEX_POWER_2_NAME, Description = EnumAuspex.AUSPEX_POWER_2_DESCRIPTION, System = EnumAuspex.AUSPEX_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumAuspex.AUSPEX_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAuspex.AUSPEX_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumAuspex.AUSPEX_POWER_3_KEY, PowerName = EnumAuspex.AUSPEX_POWER_3_NAME, Description = EnumAuspex.AUSPEX_POWER_3_DESCRIPTION, System = EnumAuspex.AUSPEX_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumAuspex.AUSPEX_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAuspex.AUSPEX_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumAuspex.AUSPEX_POWER_4_KEY, PowerName = EnumAuspex.AUSPEX_POWER_4_NAME, Description = EnumAuspex.AUSPEX_POWER_4_DESCRIPTION, System = EnumAuspex.AUSPEX_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumAuspex.AUSPEX_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAuspex.AUSPEX_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumAuspex.AUSPEX_POWER_5_KEY, PowerName = EnumAuspex.AUSPEX_POWER_5_NAME, Description = EnumAuspex.AUSPEX_POWER_5_DESCRIPTION, System = EnumAuspex.AUSPEX_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumAuspex.AUSPEX_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAuspex.AUSPEX_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumAuspex.AUSPEX_ELDER_POWER_1_KEY, PowerName = EnumAuspex.AUSPEX_ELDER_POWER_1_NAME, Description = EnumAuspex.AUSPEX_ELDER_POWER_1_DESCRIPTION, System = EnumAuspex.AUSPEX_ELDER_POWER_1_SYSTEM, ExceptionalSuccess = EnumAuspex.AUSPEX_ELDER_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumAuspex.AUSPEX_ELDER_POWER_2_KEY, PowerName = EnumAuspex.AUSPEX_ELDER_POWER_2_NAME, Description = EnumAuspex.AUSPEX_ELDER_POWER_2_DESCRIPTION, System = EnumAuspex.AUSPEX_ELDER_POWER_2_SYSTEM, ExceptionalSuccess = EnumAuspex.AUSPEX_ELDER_POWER_2_EXCEPTIONALSUCCESS }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumAuspex.AUSPEX_KEY,
                    DisciplineName = EnumAuspex.AUSPEX_NAME,
                    Description = EnumAuspex.AUSPEX_DESCRIPTION,
                    TestScore = EnumAuspex.AUSPEX_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void CelerityInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumCelerity.CELERITY_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumCelerity.CELERITY_POWER_1_KEY, PowerName = EnumCelerity.CELERITY_POWER_1_NAME, Description = EnumCelerity.CELERITY_POWER_1_DESCRIPTION, System = EnumCelerity.CELERITY_POWER_1_SYSTEM, Focus = null, FocusEffect = EnumCelerity.CELERITY_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCelerity.CELERITY_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumCelerity.CELERITY_POWER_2_KEY, PowerName = EnumCelerity.CELERITY_POWER_2_NAME, Description = EnumCelerity.CELERITY_POWER_2_DESCRIPTION, System = EnumCelerity.CELERITY_POWER_2_SYSTEM, Focus = null, FocusEffect = EnumCelerity.CELERITY_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCelerity.CELERITY_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumCelerity.CELERITY_POWER_3_KEY, PowerName = EnumCelerity.CELERITY_POWER_3_NAME, Description = EnumCelerity.CELERITY_POWER_3_DESCRIPTION, System = EnumCelerity.CELERITY_POWER_3_SYSTEM, Focus = null, FocusEffect = EnumCelerity.CELERITY_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCelerity.CELERITY_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumCelerity.CELERITY_POWER_4_KEY, PowerName = EnumCelerity.CELERITY_POWER_4_NAME, Description = EnumCelerity.CELERITY_POWER_4_DESCRIPTION, System = EnumCelerity.CELERITY_POWER_4_SYSTEM, Focus = null, FocusEffect = EnumCelerity.CELERITY_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCelerity.CELERITY_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumCelerity.CELERITY_POWER_5_KEY, PowerName = EnumCelerity.CELERITY_POWER_5_NAME, Description = EnumCelerity.CELERITY_POWER_5_DESCRIPTION, System = EnumCelerity.CELERITY_POWER_5_SYSTEM, Focus = null, FocusEffect = EnumCelerity.CELERITY_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCelerity.CELERITY_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumCelerity.CELERITY_ELDER_POWER_1_KEY, PowerName = EnumCelerity.CELERITY_ELDER_POWER_1_NAME, Description = EnumCelerity.CELERITY_ELDER_POWER_1_DESCRIPTION, System = EnumCelerity.CELERITY_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumCelerity.CELERITY_ELDER_POWER_2_KEY, PowerName = EnumCelerity.CELERITY_ELDER_POWER_2_NAME, Description = EnumCelerity.CELERITY_ELDER_POWER_2_DESCRIPTION, System = EnumCelerity.CELERITY_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumCelerity.CELERITY_KEY,
                    DisciplineName = EnumCelerity.CELERITY_NAME,
                    Description = EnumCelerity.CELERITY_DESCRIPTION,
                    TestScore = EnumCelerity.CELERITY_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ChimestryInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumChimerstry.CHIMERSTRY_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumChimerstry.CHIMERSTRY_POWER_1_KEY, PowerName = EnumChimerstry.CHIMERSTRY_POWER_1_NAME, Description = EnumChimerstry.CHIMERSTRY_POWER_1_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumChimerstry.CHIMERSTRY_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumChimerstry.CHIMERSTRY_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumChimerstry.CHIMERSTRY_POWER_2_KEY, PowerName = EnumChimerstry.CHIMERSTRY_POWER_2_NAME, Description = EnumChimerstry.CHIMERSTRY_POWER_2_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumChimerstry.CHIMERSTRY_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumChimerstry.CHIMERSTRY_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumChimerstry.CHIMERSTRY_POWER_3_KEY, PowerName = EnumChimerstry.CHIMERSTRY_POWER_3_NAME, Description = EnumChimerstry.CHIMERSTRY_POWER_3_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumChimerstry.CHIMERSTRY_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumChimerstry.CHIMERSTRY_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumChimerstry.CHIMERSTRY_POWER_4_KEY, PowerName = EnumChimerstry.CHIMERSTRY_POWER_4_NAME, Description = EnumChimerstry.CHIMERSTRY_POWER_4_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumChimerstry.CHIMERSTRY_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumChimerstry.CHIMERSTRY_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumChimerstry.CHIMERSTRY_POWER_5_KEY, PowerName = EnumChimerstry.CHIMERSTRY_POWER_5_NAME, Description = EnumChimerstry.CHIMERSTRY_POWER_5_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumChimerstry.CHIMERSTRY_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumChimerstry.CHIMERSTRY_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumChimerstry.CHIMERSTRY_ELDER_POWER_1_KEY, PowerName = EnumChimerstry.CHIMERSTRY_ELDER_POWER_1_NAME, Description = EnumChimerstry.CHIMERSTRY_ELDER_POWER_1_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumChimerstry.CHIMERSTRY_ELDER_POWER_2_KEY, PowerName = EnumChimerstry.CHIMERSTRY_ELDER_POWER_2_NAME, Description = EnumChimerstry.CHIMERSTRY_ELDER_POWER_2_DESCRIPTION, System = EnumChimerstry.CHIMERSTRY_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumChimerstry.CHIMERSTRY_KEY,
                    DisciplineName = EnumChimerstry.CHIMERSTRY_NAME,
                    Description = EnumChimerstry.CHIMERSTRY_DESCRIPTION,
                    TestScore = EnumChimerstry.CHIMERSTRY_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void DaimoinonInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumDaimonion.DAIMOINON_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumDaimonion.DAIMOINON_POWER_1_KEY, PowerName = EnumDaimonion.DAIMOINON_POWER_1_NAME, Description = EnumDaimonion.DAIMOINON_POWER_1_DESCRIPTION, System = EnumDaimonion.DAIMOINON_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumDaimonion.DAIMOINON_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDaimonion.DAIMOINON_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumDaimonion.DAIMOINON_POWER_2_KEY, PowerName = EnumDaimonion.DAIMOINON_POWER_2_NAME, Description = EnumDaimonion.DAIMOINON_POWER_2_DESCRIPTION, System = EnumDaimonion.DAIMOINON_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumDaimonion.DAIMOINON_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDaimonion.DAIMOINON_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumDaimonion.DAIMOINON_POWER_3_KEY, PowerName = EnumDaimonion.DAIMOINON_POWER_3_NAME, Description = EnumDaimonion.DAIMOINON_POWER_3_DESCRIPTION, System = EnumDaimonion.DAIMOINON_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumDaimonion.DAIMOINON_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDaimonion.DAIMOINON_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumDaimonion.DAIMOINON_POWER_4_KEY, PowerName = EnumDaimonion.DAIMOINON_POWER_4_NAME, Description = EnumDaimonion.DAIMOINON_POWER_4_DESCRIPTION, System = EnumDaimonion.DAIMOINON_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumDaimonion.DAIMOINON_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDaimonion.DAIMOINON_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumDaimonion.DAIMOINON_POWER_5_KEY, PowerName = EnumDaimonion.DAIMOINON_POWER_5_NAME, Description = EnumDaimonion.DAIMOINON_POWER_5_DESCRIPTION, System = EnumDaimonion.DAIMOINON_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumDaimonion.DAIMOINON_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDaimonion.DAIMOINON_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumDaimonion.DAIMOINON_ELDER_POWER_1_KEY, PowerName = EnumDaimonion.DAIMOINON_ELDER_POWER_1_NAME, Description = EnumDaimonion.DAIMOINON_ELDER_POWER_1_DESCRIPTION, System = EnumDaimonion.DAIMOINON_ELDER_POWER_1_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumDaimonion.DAIMOINON_KEY,
                    DisciplineName = EnumDaimonion.DAIMOINON_NAME,
                    Description = EnumDaimonion.DAIMOINON_DESCRIPTION,
                    TestScore = EnumDaimonion.DAIMOINON_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void DementationInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumDementation.DEMENTATION_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumDementation.DEMENTATION_POWER_1_KEY, PowerName = EnumDementation.DEMENTATION_POWER_1_NAME, Description = EnumDementation.DEMENTATION_POWER_1_DESCRIPTION, System = EnumDementation.DEMENTATION_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumDementation.DEMENTATION_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDementation.DEMENTATION_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumDementation.DEMENTATION_POWER_2_KEY, PowerName = EnumDementation.DEMENTATION_POWER_2_NAME, Description = EnumDementation.DEMENTATION_POWER_2_DESCRIPTION, System = EnumDementation.DEMENTATION_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumDementation.DEMENTATION_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDementation.DEMENTATION_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumDementation.DEMENTATION_POWER_3_KEY, PowerName = EnumDementation.DEMENTATION_POWER_3_NAME, Description = EnumDementation.DEMENTATION_POWER_3_DESCRIPTION, System = EnumDementation.DEMENTATION_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumDementation.DEMENTATION_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDementation.DEMENTATION_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumDementation.DEMENTATION_POWER_4_KEY, PowerName = EnumDementation.DEMENTATION_POWER_4_NAME, Description = EnumDementation.DEMENTATION_POWER_4_DESCRIPTION, System = EnumDementation.DEMENTATION_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumDementation.DEMENTATION_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDementation.DEMENTATION_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumDementation.DEMENTATION_POWER_5_KEY, PowerName = EnumDementation.DEMENTATION_POWER_5_NAME, Description = EnumDementation.DEMENTATION_POWER_5_DESCRIPTION, System = EnumDementation.DEMENTATION_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumDementation.DEMENTATION_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDementation.DEMENTATION_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumDementation.DEMENTATION_ELDER_POWER_1_KEY, PowerName = EnumDementation.DEMENTATION_ELDER_POWER_1_NAME, Description = EnumDementation.DEMENTATION_ELDER_POWER_1_DESCRIPTION, System = EnumDementation.DEMENTATION_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumDementation.DEMENTATION_ELDER_POWER_2_KEY, PowerName = EnumDementation.DEMENTATION_ELDER_POWER_2_NAME, Description = EnumDementation.DEMENTATION_ELDER_POWER_2_DESCRIPTION, System = EnumDementation.DEMENTATION_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumDementation.DEMENTATION_KEY,
                    DisciplineName = EnumDementation.DEMENTATION_NAME,
                    Description = EnumDementation.DEMENTATION_DESCRIPTION,
                    TestScore = EnumDementation.DEMENTATION_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void DominateInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumDominate.DOMINATE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumDominate.DOMINATE_POWER_1_KEY, PowerName = EnumDominate.DOMINATE_POWER_1_NAME, Description = EnumDominate.DOMINATE_POWER_1_DESCRIPTION, System = EnumDominate.DOMINATE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumDominate.DOMINATE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDominate.DOMINATE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumDominate.DOMINATE_POWER_2_KEY, PowerName = EnumDominate.DOMINATE_POWER_2_NAME, Description = EnumDominate.DOMINATE_POWER_2_DESCRIPTION, System = EnumDominate.DOMINATE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumDominate.DOMINATE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDominate.DOMINATE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumDominate.DOMINATE_POWER_3_KEY, PowerName = EnumDominate.DOMINATE_POWER_3_NAME, Description = EnumDominate.DOMINATE_POWER_3_DESCRIPTION, System = EnumDominate.DOMINATE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumDominate.DOMINATE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDominate.DOMINATE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumDominate.DOMINATE_POWER_4_KEY, PowerName = EnumDominate.DOMINATE_POWER_4_NAME, Description = EnumDominate.DOMINATE_POWER_4_DESCRIPTION, System = EnumDominate.DOMINATE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumDominate.DOMINATE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDominate.DOMINATE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumDominate.DOMINATE_POWER_5_KEY, PowerName = EnumDominate.DOMINATE_POWER_5_NAME, Description = EnumDominate.DOMINATE_POWER_5_DESCRIPTION, System = EnumDominate.DOMINATE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumDominate.DOMINATE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumDominate.DOMINATE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumDominate.DOMINATE_ELDER_POWER_1_KEY, PowerName = EnumDominate.DOMINATE_ELDER_POWER_1_NAME, Description = EnumDominate.DOMINATE_ELDER_POWER_1_DESCRIPTION, System = EnumDominate.DOMINATE_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumDominate.DOMINATE_ELDER_POWER_2_KEY, PowerName = EnumDominate.DOMINATE_ELDER_POWER_2_NAME, Description = EnumDominate.DOMINATE_ELDER_POWER_2_DESCRIPTION, System = EnumDominate.DOMINATE_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumDominate.DOMINATE_KEY,
                    DisciplineName = EnumDominate.DOMINATE_NAME,
                    Description = EnumDominate.DOMINATE_DESCRIPTION,
                    TestScore = EnumDominate.DOMINATE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void FortitudeInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumFortitude.FORTITUDE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumFortitude.FORTITUDE_POWER_1_KEY, PowerName = EnumFortitude.FORTITUDE_POWER_1_NAME, Description = EnumFortitude.FORTITUDE_POWER_1_DESCRIPTION, System = EnumFortitude.FORTITUDE_POWER_1_SYSTEM, Focus = null, FocusEffect = EnumFortitude.FORTITUDE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFortitude.FORTITUDE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumFortitude.FORTITUDE_POWER_2_KEY, PowerName = EnumFortitude.FORTITUDE_POWER_2_NAME, Description = EnumFortitude.FORTITUDE_POWER_2_DESCRIPTION, System = EnumFortitude.FORTITUDE_POWER_2_SYSTEM, Focus = null, FocusEffect = EnumFortitude.FORTITUDE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFortitude.FORTITUDE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumFortitude.FORTITUDE_POWER_3_KEY, PowerName = EnumFortitude.FORTITUDE_POWER_3_NAME, Description = EnumFortitude.FORTITUDE_POWER_3_DESCRIPTION, System = EnumFortitude.FORTITUDE_POWER_3_SYSTEM, Focus = null, FocusEffect = EnumFortitude.FORTITUDE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFortitude.FORTITUDE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumFortitude.FORTITUDE_POWER_4_KEY, PowerName = EnumFortitude.FORTITUDE_POWER_4_NAME, Description = EnumFortitude.FORTITUDE_POWER_4_DESCRIPTION, System = EnumFortitude.FORTITUDE_POWER_4_SYSTEM, Focus = null, FocusEffect = EnumFortitude.FORTITUDE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFortitude.FORTITUDE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumFortitude.FORTITUDE_POWER_5_KEY, PowerName = EnumFortitude.FORTITUDE_POWER_5_NAME, Description = EnumFortitude.FORTITUDE_POWER_5_DESCRIPTION, System = EnumFortitude.FORTITUDE_POWER_5_SYSTEM, Focus = null, FocusEffect = EnumFortitude.FORTITUDE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFortitude.FORTITUDE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumFortitude.FORTITUDE_ELDER_POWER_1_KEY, PowerName = EnumFortitude.FORTITUDE_ELDER_POWER_1_NAME, Description = EnumFortitude.FORTITUDE_ELDER_POWER_1_DESCRIPTION, System = EnumFortitude.FORTITUDE_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumFortitude.FORTITUDE_ELDER_POWER_2_KEY, PowerName = EnumFortitude.FORTITUDE_ELDER_POWER_2_NAME, Description = EnumFortitude.FORTITUDE_ELDER_POWER_2_DESCRIPTION, System = EnumFortitude.FORTITUDE_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumFortitude.FORTITUDE_KEY,
                    DisciplineName = EnumFortitude.FORTITUDE_NAME,
                    Description = EnumFortitude.FORTITUDE_DESCRIPTION,
                    TestScore = EnumFortitude.FORTITUDE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void MelpomineeInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumMelpominee.MELPOMINEE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumMelpominee.MELPOMINEE_POWER_1_KEY, PowerName = EnumMelpominee.MELPOMINEE_POWER_1_NAME, Description = EnumMelpominee.MELPOMINEE_POWER_1_DESCRIPTION, System = EnumMelpominee.MELPOMINEE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumMelpominee.MELPOMINEE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMelpominee.MELPOMINEE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumMelpominee.MELPOMINEE_POWER_2_KEY, PowerName = EnumMelpominee.MELPOMINEE_POWER_2_NAME, Description = EnumMelpominee.MELPOMINEE_POWER_2_DESCRIPTION, System = EnumMelpominee.MELPOMINEE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumMelpominee.MELPOMINEE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMelpominee.MELPOMINEE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumMelpominee.MELPOMINEE_POWER_3_KEY, PowerName = EnumMelpominee.MELPOMINEE_POWER_3_NAME, Description = EnumMelpominee.MELPOMINEE_POWER_3_DESCRIPTION, System = EnumMelpominee.MELPOMINEE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumMelpominee.MELPOMINEE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMelpominee.MELPOMINEE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumMelpominee.MELPOMINEE_POWER_4_KEY, PowerName = EnumMelpominee.MELPOMINEE_POWER_4_NAME, Description = EnumMelpominee.MELPOMINEE_POWER_4_DESCRIPTION, System = EnumMelpominee.MELPOMINEE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumMelpominee.MELPOMINEE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMelpominee.MELPOMINEE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumMelpominee.MELPOMINEE_POWER_5_KEY, PowerName = EnumMelpominee.MELPOMINEE_POWER_5_NAME, Description = EnumMelpominee.MELPOMINEE_POWER_5_DESCRIPTION, System = EnumMelpominee.MELPOMINEE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumMelpominee.MELPOMINEE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMelpominee.MELPOMINEE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumMelpominee.MELPOMINEE_ELDER_POWER_1_KEY, PowerName = EnumMelpominee.MELPOMINEE_ELDER_POWER_1_NAME, Description = EnumMelpominee.MELPOMINEE_ELDER_POWER_1_NAME, System = EnumMelpominee.MELPOMINEE_ELDER_POWER_1_NAME },
                    new Power { Level = 6, PowerKey = EnumMelpominee.MELPOMINEE_ELDER_POWER_2_KEY, PowerName = EnumMelpominee.MELPOMINEE_ELDER_POWER_2_NAME, Description = EnumMelpominee.MELPOMINEE_ELDER_POWER_2_NAME, System = EnumMelpominee.MELPOMINEE_ELDER_POWER_2_NAME },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumMelpominee.MELPOMINEE_KEY,
                    DisciplineName = EnumMelpominee.MELPOMINEE_NAME,
                    Description = EnumMelpominee.MELPOMINEE_DESCRIPTION,
                    TestScore = EnumMelpominee.MELPOMINEE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void MytherceriaInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumMystherceria.MYSTHERCERIA_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumMystherceria.MYSTHERCERIA_POWER_1_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_POWER_1_NAME, Description = EnumMystherceria.MYSTHERCERIA_POWER_1_DESCRIPTION, System = EnumMystherceria.MYSTHERCERIA_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumMystherceria.MYSTHERCERIA_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMystherceria.MYSTHERCERIA_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumMystherceria.MYSTHERCERIA_POWER_2_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_POWER_2_NAME, Description = EnumMystherceria.MYSTHERCERIA_POWER_2_DESCRIPTION, System = EnumMystherceria.MYSTHERCERIA_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumMystherceria.MYSTHERCERIA_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMystherceria.MYSTHERCERIA_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumMystherceria.MYSTHERCERIA_POWER_3_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_POWER_3_NAME, Description = EnumMystherceria.MYSTHERCERIA_POWER_3_DESCRIPTION, System = EnumMystherceria.MYSTHERCERIA_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumMystherceria.MYSTHERCERIA_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMystherceria.MYSTHERCERIA_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumMystherceria.MYSTHERCERIA_POWER_4_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_POWER_4_NAME, Description = EnumMystherceria.MYSTHERCERIA_POWER_4_DESCRIPTION, System = EnumMystherceria.MYSTHERCERIA_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumMystherceria.MYSTHERCERIA_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMystherceria.MYSTHERCERIA_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumMystherceria.MYSTHERCERIA_POWER_5_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_POWER_5_NAME, Description = EnumMystherceria.MYSTHERCERIA_POWER_5_DESCRIPTION, System = EnumMystherceria.MYSTHERCERIA_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumMystherceria.MYSTHERCERIA_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMystherceria.MYSTHERCERIA_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumMystherceria.MYSTHERCERIA_ELDER_POWER_1_KEY, PowerName = EnumMystherceria.MYSTHERCERIA_ELDER_POWER_1_NAME, Description = EnumMystherceria.MYSTHERCERIA_ELDER_POWER_1_NAME, System = EnumMystherceria.MYSTHERCERIA_ELDER_POWER_1_NAME }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumMystherceria.MYSTHERCERIA_KEY,
                    DisciplineName = EnumMystherceria.MYSTHERCERIA_NAME,
                    Description = EnumMystherceria.MYSTHERCERIA_DESCRIPTION,
                    TestScore = EnumMystherceria.MYSTHERCERIA_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ObeahInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumObeah.OBEAH_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumObeah.OBEAH_POWER_1_KEY, PowerName = EnumObeah.OBEAH_POWER_1_NAME, Description = EnumObeah.OBEAH_POWER_1_DESCRIPTION, System = EnumObeah.OBEAH_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumObeah.OBEAH_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObeah.OBEAH_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumObeah.OBEAH_POWER_2_KEY, PowerName = EnumObeah.OBEAH_POWER_2_NAME, Description = EnumObeah.OBEAH_POWER_2_DESCRIPTION, System = EnumObeah.OBEAH_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumObeah.OBEAH_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObeah.OBEAH_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumObeah.OBEAH_POWER_3_KEY, PowerName = EnumObeah.OBEAH_POWER_3_NAME, Description = EnumObeah.OBEAH_POWER_3_DESCRIPTION, System = EnumObeah.OBEAH_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumObeah.OBEAH_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObeah.OBEAH_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumObeah.OBEAH_POWER_4_KEY, PowerName = EnumObeah.OBEAH_POWER_4_NAME, Description = EnumObeah.OBEAH_POWER_4_DESCRIPTION, System = EnumObeah.OBEAH_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumObeah.OBEAH_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObeah.OBEAH_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumObeah.OBEAH_POWER_5_KEY, PowerName = EnumObeah.OBEAH_POWER_5_NAME, Description = EnumObeah.OBEAH_POWER_5_DESCRIPTION, System = EnumObeah.OBEAH_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumObeah.OBEAH_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObeah.OBEAH_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumObeah.OBEAH_ELDER_POWER_1_KEY, PowerName = EnumObeah.OBEAH_ELDER_POWER_1_NAME, Description = EnumObeah.OBEAH_ELDER_POWER_1_DESCRIPTION, System = EnumObeah.OBEAH_ELDER_POWER_1_SYSTEM },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumObeah.OBEAH_KEY,
                    DisciplineName = EnumObeah.OBEAH_NAME,
                    Description = EnumObeah.OBEAH_DESCRIPTION,
                    TestScore = EnumObeah.OBEAH_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ObfuscateInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumObfuscate.OBFUSCATE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumObfuscate.OBFUSCATE_POWER_1_KEY, PowerName = EnumObfuscate.OBFUSCATE_POWER_1_NAME, Description = EnumObfuscate.OBFUSCATE_POWER_1_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumObfuscate.OBFUSCATE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObfuscate.OBFUSCATE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumObfuscate.OBFUSCATE_POWER_2_KEY, PowerName = EnumObfuscate.OBFUSCATE_POWER_2_NAME, Description = EnumObfuscate.OBFUSCATE_POWER_2_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumObfuscate.OBFUSCATE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObfuscate.OBFUSCATE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumObfuscate.OBFUSCATE_POWER_3_KEY, PowerName = EnumObfuscate.OBFUSCATE_POWER_3_NAME, Description = EnumObfuscate.OBFUSCATE_POWER_3_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumObfuscate.OBFUSCATE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObfuscate.OBFUSCATE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumObfuscate.OBFUSCATE_POWER_4_KEY, PowerName = EnumObfuscate.OBFUSCATE_POWER_4_NAME, Description = EnumObfuscate.OBFUSCATE_POWER_4_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumObfuscate.OBFUSCATE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObfuscate.OBFUSCATE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumObfuscate.OBFUSCATE_POWER_5_KEY, PowerName = EnumObfuscate.OBFUSCATE_POWER_5_NAME, Description = EnumObfuscate.OBFUSCATE_POWER_5_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumObfuscate.OBFUSCATE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObfuscate.OBFUSCATE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumObfuscate.OBFUSCATE_ELDER_POWER_1_KEY, PowerName = EnumObfuscate.OBFUSCATE_ELDER_POWER_1_NAME, Description = EnumObfuscate.OBFUSCATE_ELDER_POWER_1_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumObfuscate.OBFUSCATE_ELDER_POWER_2_KEY, PowerName = EnumObfuscate.OBFUSCATE_ELDER_POWER_2_NAME, Description = EnumObfuscate.OBFUSCATE_ELDER_POWER_2_DESCRIPTION, System = EnumObfuscate.OBFUSCATE_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumObfuscate.OBFUSCATE_KEY,
                    DisciplineName = EnumObfuscate.OBFUSCATE_NAME,
                    Description = EnumObfuscate.OBFUSCATE_DESCRIPTION,
                    TestScore = EnumObfuscate.OBFUSCATE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ObtenebrationInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumObtenebration.OBTENEBRATION_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumObtenebration.OBTENEBRATION_POWER_1_KEY, PowerName = EnumObtenebration.OBTENEBRATION_POWER_1_NAME, Description = EnumObtenebration.OBTENEBRATION_POWER_1_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumObtenebration.OBTENEBRATION_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObtenebration.OBTENEBRATION_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumObtenebration.OBTENEBRATION_POWER_2_KEY, PowerName = EnumObtenebration.OBTENEBRATION_POWER_2_NAME, Description = EnumObtenebration.OBTENEBRATION_POWER_2_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumObtenebration.OBTENEBRATION_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObtenebration.OBTENEBRATION_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumObtenebration.OBTENEBRATION_POWER_3_KEY, PowerName = EnumObtenebration.OBTENEBRATION_POWER_3_NAME, Description = EnumObtenebration.OBTENEBRATION_POWER_3_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumObtenebration.OBTENEBRATION_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObtenebration.OBTENEBRATION_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumObtenebration.OBTENEBRATION_POWER_4_KEY, PowerName = EnumObtenebration.OBTENEBRATION_POWER_4_NAME, Description = EnumObtenebration.OBTENEBRATION_POWER_4_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumObtenebration.OBTENEBRATION_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObtenebration.OBTENEBRATION_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumObtenebration.OBTENEBRATION_POWER_5_KEY, PowerName = EnumObtenebration.OBTENEBRATION_POWER_5_NAME, Description = EnumObtenebration.OBTENEBRATION_POWER_5_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumObtenebration.OBTENEBRATION_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumObtenebration.OBTENEBRATION_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumObtenebration.OBTENEBRATION_ELDER_POWER_1_KEY, PowerName = EnumObtenebration.OBTENEBRATION_ELDER_POWER_1_NAME, Description = EnumObtenebration.OBTENEBRATION_ELDER_POWER_1_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumObtenebration.OBTENEBRATION_ELDER_POWER_2_KEY, PowerName = EnumObtenebration.OBTENEBRATION_ELDER_POWER_2_NAME, Description = EnumObtenebration.OBTENEBRATION_ELDER_POWER_2_DESCRIPTION, System = EnumObtenebration.OBTENEBRATION_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumObtenebration.OBTENEBRATION_KEY,
                    DisciplineName = EnumObtenebration.OBTENEBRATION_NAME,
                    Description = EnumObtenebration.OBTENEBRATION_DESCRIPTION,
                    TestScore = EnumObtenebration.OBTENEBRATION_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void PotenceInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumPotence.POTENCE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumPotence.POTENCE_POWER_1_KEY, PowerName = EnumPotence.POTENCE_POWER_1_NAME, Description = EnumPotence.POTENCE_POWER_1_DESCRIPTION, System = EnumPotence.POTENCE_POWER_1_SYSTEM, Focus = null, FocusEffect = EnumPotence.POTENCE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPotence.POTENCE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumPotence.POTENCE_POWER_2_KEY, PowerName = EnumPotence.POTENCE_POWER_2_NAME, Description = EnumPotence.POTENCE_POWER_2_DESCRIPTION, System = EnumPotence.POTENCE_POWER_2_SYSTEM, Focus = null, FocusEffect = EnumPotence.POTENCE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPotence.POTENCE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumPotence.POTENCE_POWER_3_KEY, PowerName = EnumPotence.POTENCE_POWER_3_NAME, Description = EnumPotence.POTENCE_POWER_3_DESCRIPTION, System = EnumPotence.POTENCE_POWER_3_SYSTEM, Focus = null, FocusEffect = EnumPotence.POTENCE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPotence.POTENCE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumPotence.POTENCE_POWER_4_KEY, PowerName = EnumPotence.POTENCE_POWER_4_NAME, Description = EnumPotence.POTENCE_POWER_4_DESCRIPTION, System = EnumPotence.POTENCE_POWER_4_SYSTEM, Focus = null, FocusEffect = EnumPotence.POTENCE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPotence.POTENCE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumPotence.POTENCE_POWER_5_KEY, PowerName = EnumPotence.POTENCE_POWER_5_NAME, Description = EnumPotence.POTENCE_POWER_5_DESCRIPTION, System = EnumPotence.POTENCE_POWER_5_SYSTEM, Focus = null, FocusEffect = EnumPotence.POTENCE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPotence.POTENCE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumPotence.POTENCE_ELDER_POWER_1_KEY, PowerName = EnumPotence.POTENCE_ELDER_POWER_1_NAME, Description = EnumPotence.POTENCE_ELDER_POWER_1_DESCRIPTION, System = EnumPotence.POTENCE_ELDER_POWER_1_SYSTEM, ExceptionalSuccess = EnumPotence.POTENCE_ELDER_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumPotence.POTENCE_ELDER_POWER_2_KEY, PowerName = EnumPotence.POTENCE_ELDER_POWER_2_NAME, Description = EnumPotence.POTENCE_ELDER_POWER_2_DESCRIPTION, System = EnumPotence.POTENCE_ELDER_POWER_2_SYSTEM },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumPotence.POTENCE_KEY,
                    DisciplineName = EnumPotence.POTENCE_NAME,
                    Description = EnumPotence.POTENCE_DESCRIPTION,
                    TestScore = EnumPotence.POTENCE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void PresenceInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumPresence.PRESENCE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumPresence.PRESENCE_POWER_1_KEY, PowerName = EnumPresence.PRESENCE_POWER_1_NAME, Description = EnumPresence.PRESENCE_POWER_1_DESCRIPTION, System = EnumPresence.PRESENCE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumPresence.PRESENCE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPresence.PRESENCE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumPresence.PRESENCE_POWER_2_KEY, PowerName = EnumPresence.PRESENCE_POWER_2_NAME, Description = EnumPresence.PRESENCE_POWER_2_DESCRIPTION, System = EnumPresence.PRESENCE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumPresence.PRESENCE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPresence.PRESENCE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumPresence.PRESENCE_POWER_3_KEY, PowerName = EnumPresence.PRESENCE_POWER_3_NAME, Description = EnumPresence.PRESENCE_POWER_3_DESCRIPTION, System = EnumPresence.PRESENCE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumPresence.PRESENCE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPresence.PRESENCE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumPresence.PRESENCE_POWER_4_KEY, PowerName = EnumPresence.PRESENCE_POWER_4_NAME, Description = EnumPresence.PRESENCE_POWER_4_DESCRIPTION, System = EnumPresence.PRESENCE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumPresence.PRESENCE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPresence.PRESENCE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumPresence.PRESENCE_POWER_5_KEY, PowerName = EnumPresence.PRESENCE_POWER_5_NAME, Description = EnumPresence.PRESENCE_POWER_5_DESCRIPTION, System = EnumPresence.PRESENCE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumPresence.PRESENCE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumPresence.PRESENCE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumPresence.PRESENCE_ELDER_POWER_1_KEY, PowerName = EnumPresence.PRESENCE_ELDER_POWER_1_NAME, Description = EnumPresence.PRESENCE_ELDER_POWER_1_DESCRIPTION, System = EnumPresence.PRESENCE_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumPresence.PRESENCE_ELDER_POWER_2_KEY, PowerName = EnumPresence.PRESENCE_ELDER_POWER_2_NAME, Description = EnumPresence.PRESENCE_ELDER_POWER_2_DESCRIPTION, System = EnumPresence.PRESENCE_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumPresence.PRESENCE_KEY,
                    DisciplineName = EnumPresence.PRESENCE_NAME,
                    Description = EnumPresence.PRESENCE_DESCRIPTION,
                    TestScore = EnumPresence.PRESENCE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ProteanInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumProtean.PROTEAN_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumProtean.PROTEAN_POWER_1_KEY, PowerName = EnumProtean.PROTEAN_POWER_1_NAME, Description = EnumProtean.PROTEAN_POWER_1_DESCRIPTION, System = EnumProtean.PROTEAN_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumProtean.PROTEAN_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumProtean.PROTEAN_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumProtean.PROTEAN_POWER_2_KEY, PowerName = EnumProtean.PROTEAN_POWER_2_NAME, Description = EnumProtean.PROTEAN_POWER_2_DESCRIPTION, System = EnumProtean.PROTEAN_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumProtean.PROTEAN_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumProtean.PROTEAN_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumProtean.PROTEAN_POWER_3_KEY, PowerName = EnumProtean.PROTEAN_POWER_3_NAME, Description = EnumProtean.PROTEAN_POWER_3_DESCRIPTION, System = EnumProtean.PROTEAN_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumProtean.PROTEAN_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumProtean.PROTEAN_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumProtean.PROTEAN_POWER_4_KEY, PowerName = EnumProtean.PROTEAN_POWER_4_NAME, Description = EnumProtean.PROTEAN_POWER_4_DESCRIPTION, System = EnumProtean.PROTEAN_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumProtean.PROTEAN_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumProtean.PROTEAN_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumProtean.PROTEAN_POWER_5_KEY, PowerName = EnumProtean.PROTEAN_POWER_5_NAME, Description = EnumProtean.PROTEAN_POWER_5_DESCRIPTION, System = EnumProtean.PROTEAN_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumProtean.PROTEAN_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumProtean.PROTEAN_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumProtean.PROTEAN_ELDER_POWER_1_KEY, PowerName = EnumProtean.PROTEAN_ELDER_POWER_1_NAME, Description = EnumProtean.PROTEAN_ELDER_POWER_1_DESCRIPTION, System = EnumProtean.PROTEAN_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumProtean.PROTEAN_ELDER_POWER_2_KEY, PowerName = EnumProtean.PROTEAN_ELDER_POWER_2_NAME, Description = EnumProtean.PROTEAN_ELDER_POWER_2_DESCRIPTION, System = EnumProtean.PROTEAN_ELDER_POWER_2_SYSTEM },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumProtean.PROTEAN_KEY,
                    DisciplineName = EnumProtean.PROTEAN_NAME,
                    Description = EnumProtean.PROTEAN_DESCRIPTION,
                    TestScore = EnumProtean.PROTEAN_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void QuietusInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumQuietus.QUIESTUS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumQuietus.QUIESTUS_POWER_1_KEY, PowerName = EnumQuietus.QUIESTUS_POWER_1_NAME, Description = EnumQuietus.QUIESTUS_POWER_1_DESCRIPTION, System = EnumQuietus.QUIESTUS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumQuietus.QUIESTUS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumQuietus.QUIESTUS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumQuietus.QUIESTUS_POWER_2_KEY, PowerName = EnumQuietus.QUIESTUS_POWER_2_NAME, Description = EnumQuietus.QUIESTUS_POWER_2_DESCRIPTION, System = EnumQuietus.QUIESTUS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumQuietus.QUIESTUS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumQuietus.QUIESTUS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumQuietus.QUIESTUS_POWER_3_KEY, PowerName = EnumQuietus.QUIESTUS_POWER_3_NAME, Description = EnumQuietus.QUIESTUS_POWER_3_DESCRIPTION, System = EnumQuietus.QUIESTUS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumQuietus.QUIESTUS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumQuietus.QUIESTUS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumQuietus.QUIESTUS_POWER_4_KEY, PowerName = EnumQuietus.QUIESTUS_POWER_4_NAME, Description = EnumQuietus.QUIESTUS_POWER_4_DESCRIPTION, System = EnumQuietus.QUIESTUS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumQuietus.QUIESTUS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumQuietus.QUIESTUS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumQuietus.QUIESTUS_POWER_5_KEY, PowerName = EnumQuietus.QUIESTUS_POWER_5_NAME, Description = EnumQuietus.QUIESTUS_POWER_5_DESCRIPTION, System = EnumQuietus.QUIESTUS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumQuietus.QUIESTUS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumQuietus.QUIESTUS_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumQuietus.QUIESTUS_ELDER_POWER_1_KEY, PowerName = EnumQuietus.QUIESTUS_ELDER_POWER_1_NAME, Description = EnumQuietus.QUIESTUS_ELDER_POWER_1_DESCRIPTION, System = EnumQuietus.QUIESTUS_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumQuietus.QUIESTUS_ELDER_POWER_2_KEY, PowerName = EnumQuietus.QUIESTUS_ELDER_POWER_2_NAME, Description = EnumQuietus.QUIESTUS_ELDER_POWER_2_DESCRIPTION, System = EnumQuietus.QUIESTUS_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumQuietus.QUIESTUS_KEY,
                    DisciplineName = EnumQuietus.QUIESTUS_NAME,
                    Description = EnumQuietus.QUIESTUS_DESCRIPTION,
                    TestScore = EnumQuietus.QUIESTUS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void SerpentisInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumSerpentis.SERPENTIS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumSerpentis.SERPENTIS_POWER_1_KEY, PowerName = EnumSerpentis.SERPENTIS_POWER_1_NAME, Description = EnumSerpentis.SERPENTIS_POWER_1_DESCRIPTION, System = EnumSerpentis.SERPENTIS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumSerpentis.SERPENTIS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSerpentis.SERPENTIS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumSerpentis.SERPENTIS_POWER_2_KEY, PowerName = EnumSerpentis.SERPENTIS_POWER_2_NAME, Description = EnumSerpentis.SERPENTIS_POWER_2_DESCRIPTION, System = EnumSerpentis.SERPENTIS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumSerpentis.SERPENTIS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSerpentis.SERPENTIS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumSerpentis.SERPENTIS_POWER_3_KEY, PowerName = EnumSerpentis.SERPENTIS_POWER_3_NAME, Description = EnumSerpentis.SERPENTIS_POWER_3_DESCRIPTION, System = EnumSerpentis.SERPENTIS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumSerpentis.SERPENTIS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSerpentis.SERPENTIS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumSerpentis.SERPENTIS_POWER_4_KEY, PowerName = EnumSerpentis.SERPENTIS_POWER_4_NAME, Description = EnumSerpentis.SERPENTIS_POWER_4_DESCRIPTION, System = EnumSerpentis.SERPENTIS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumSerpentis.SERPENTIS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSerpentis.SERPENTIS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumSerpentis.SERPENTIS_POWER_5_KEY, PowerName = EnumSerpentis.SERPENTIS_POWER_5_NAME, Description = EnumSerpentis.SERPENTIS_POWER_5_DESCRIPTION, System = EnumSerpentis.SERPENTIS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumSerpentis.SERPENTIS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSerpentis.SERPENTIS_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumSerpentis.SERPENTIS_ELDER_POWER_1_KEY, PowerName = EnumSerpentis.SERPENTIS_ELDER_POWER_1_NAME, Description = EnumSerpentis.SERPENTIS_ELDER_POWER_1_DESCRIPTION, System = EnumSerpentis.SERPENTIS_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumSerpentis.SERPENTIS_ELDER_POWER_2_KEY, PowerName = EnumSerpentis.SERPENTIS_ELDER_POWER_2_NAME, Description = EnumSerpentis.SERPENTIS_ELDER_POWER_2_DESCRIPTION, System = EnumSerpentis.SERPENTIS_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumSerpentis.SERPENTIS_KEY,
                    DisciplineName = EnumSerpentis.SERPENTIS_NAME,
                    Description = EnumSerpentis.SERPENTIS_DESCRIPTION,
                    TestScore = EnumSerpentis.SERPENTIS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void TemporisInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumTemporis.TEMPORIS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumTemporis.TEMPORIS_POWER_1_KEY, PowerName = EnumTemporis.TEMPORIS_POWER_1_NAME, Description = EnumTemporis.TEMPORIS_POWER_1_DESCRIPTION, System = EnumTemporis.TEMPORIS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY), FocusEffect = EnumTemporis.TEMPORIS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTemporis.TEMPORIS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumTemporis.TEMPORIS_POWER_2_KEY, PowerName = EnumTemporis.TEMPORIS_POWER_2_NAME, Description = EnumTemporis.TEMPORIS_POWER_2_DESCRIPTION, System = EnumTemporis.TEMPORIS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY), FocusEffect = EnumTemporis.TEMPORIS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTemporis.TEMPORIS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumTemporis.TEMPORIS_POWER_3_KEY, PowerName = EnumTemporis.TEMPORIS_POWER_3_NAME, Description = EnumTemporis.TEMPORIS_POWER_3_DESCRIPTION, System = EnumTemporis.TEMPORIS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY), FocusEffect = EnumTemporis.TEMPORIS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTemporis.TEMPORIS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumTemporis.TEMPORIS_POWER_4_KEY, PowerName = EnumTemporis.TEMPORIS_POWER_4_NAME, Description = EnumTemporis.TEMPORIS_POWER_4_DESCRIPTION, System = EnumTemporis.TEMPORIS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY), FocusEffect = EnumTemporis.TEMPORIS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTemporis.TEMPORIS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumTemporis.TEMPORIS_POWER_5_KEY, PowerName = EnumTemporis.TEMPORIS_POWER_5_NAME, Description = EnumTemporis.TEMPORIS_POWER_5_DESCRIPTION, System = EnumTemporis.TEMPORIS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.PHYSICAL_STAMINA_KEY), FocusEffect = EnumTemporis.TEMPORIS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTemporis.TEMPORIS_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumTemporis.TEMPORIS_ELDER_POWER_1_KEY, PowerName = EnumTemporis.TEMPORIS_ELDER_POWER_1_NAME, Description = EnumTemporis.TEMPORIS_ELDER_POWER_1_DESCRIPTION, System = EnumTemporis.TEMPORIS_ELDER_POWER_1_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumTemporis.TEMPORIS_KEY,
                    DisciplineName = EnumTemporis.TEMPORIS_NAME,
                    Description = EnumTemporis.TEMPORIS_DESCRIPTION,
                    TestScore = EnumTemporis.TEMPORIS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ThanatosisInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumThanatosis.THANATOSIS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumThanatosis.THANATOSIS_POWER_1_KEY, PowerName = EnumThanatosis.THANATOSIS_POWER_1_NAME, Description = EnumThanatosis.THANATOSIS_POWER_1_DESCRIPTION, System = EnumThanatosis.THANATOSIS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumThanatosis.THANATOSIS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumThanatosis.THANATOSIS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumThanatosis.THANATOSIS_POWER_2_KEY, PowerName = EnumThanatosis.THANATOSIS_POWER_2_NAME, Description = EnumThanatosis.THANATOSIS_POWER_2_DESCRIPTION, System = EnumThanatosis.THANATOSIS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumThanatosis.THANATOSIS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumThanatosis.THANATOSIS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumThanatosis.THANATOSIS_POWER_3_KEY, PowerName = EnumThanatosis.THANATOSIS_POWER_3_NAME, Description = EnumThanatosis.THANATOSIS_POWER_3_DESCRIPTION, System = EnumThanatosis.THANATOSIS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumThanatosis.THANATOSIS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumThanatosis.THANATOSIS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumThanatosis.THANATOSIS_POWER_4_KEY, PowerName = EnumThanatosis.THANATOSIS_POWER_4_NAME, Description = EnumThanatosis.THANATOSIS_POWER_4_DESCRIPTION, System = EnumThanatosis.THANATOSIS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumThanatosis.THANATOSIS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumThanatosis.THANATOSIS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumThanatosis.THANATOSIS_POWER_5_KEY, PowerName = EnumThanatosis.THANATOSIS_POWER_5_NAME, Description = EnumThanatosis.THANATOSIS_POWER_5_DESCRIPTION, System = EnumThanatosis.THANATOSIS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumThanatosis.THANATOSIS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumThanatosis.THANATOSIS_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumThanatosis.THANATOSIS_ELDER_POWER_1_KEY, PowerName = EnumThanatosis.THANATOSIS_ELDER_POWER_1_NAME, Description = EnumThanatosis.THANATOSIS_ELDER_POWER_1_DESCRIPTION, System = EnumThanatosis.THANATOSIS_ELDER_POWER_1_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumThanatosis.THANATOSIS_KEY,
                    DisciplineName = EnumThanatosis.THANATOSIS_NAME,
                    Description = EnumThanatosis.THANATOSIS_DESCRIPTION,
                    TestScore = EnumThanatosis.THANATOSIS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ValerenInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumValeren.VALEREN_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumValeren.VALEREN_POWER_1_KEY, PowerName = EnumValeren.VALEREN_POWER_1_NAME, Description = EnumValeren.VALEREN_POWER_1_DESCRIPTION, System = EnumValeren.VALEREN_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumValeren.VALEREN_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumValeren.VALEREN_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumValeren.VALEREN_POWER_2_KEY, PowerName = EnumValeren.VALEREN_POWER_2_NAME, Description = EnumValeren.VALEREN_POWER_2_DESCRIPTION, System = EnumValeren.VALEREN_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumValeren.VALEREN_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumValeren.VALEREN_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumValeren.VALEREN_POWER_3_KEY, PowerName = EnumValeren.VALEREN_POWER_3_NAME, Description = EnumValeren.VALEREN_POWER_3_DESCRIPTION, System = EnumValeren.VALEREN_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumValeren.VALEREN_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumValeren.VALEREN_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumValeren.VALEREN_POWER_4_KEY, PowerName = EnumValeren.VALEREN_POWER_4_NAME, Description = EnumValeren.VALEREN_POWER_4_DESCRIPTION, System = EnumValeren.VALEREN_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumValeren.VALEREN_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumValeren.VALEREN_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumValeren.VALEREN_POWER_5_KEY, PowerName = EnumValeren.VALEREN_POWER_5_NAME, Description = EnumValeren.VALEREN_POWER_5_DESCRIPTION, System = EnumValeren.VALEREN_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumValeren.VALEREN_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumValeren.VALEREN_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumValeren.VALEREN_ELDER_POWER_1_KEY, PowerName = EnumValeren.VALEREN_ELDER_POWER_1_NAME, Description = EnumValeren.VALEREN_ELDER_POWER_1_DESCRIPTION, System = EnumValeren.VALEREN_ELDER_POWER_1_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumValeren.VALEREN_KEY,
                    DisciplineName = EnumValeren.VALEREN_NAME,
                    Description = EnumValeren.VALEREN_DESCRIPTION,
                    TestScore = EnumValeren.VALEREN_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void VicissitudeInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumVicissitude.VICISSITUDE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumVicissitude.VICISSITUDE_POWER_1_KEY, PowerName = EnumVicissitude.VICISSITUDE_POWER_1_NAME, Description = EnumVicissitude.VICISSITUDE_POWER_1_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumVicissitude.VICISSITUDE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumVicissitude.VICISSITUDE_POWER_2_KEY, PowerName = EnumVicissitude.VICISSITUDE_POWER_2_NAME, Description = EnumVicissitude.VICISSITUDE_POWER_2_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumVicissitude.VICISSITUDE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumVicissitude.VICISSITUDE_POWER_3_KEY, PowerName = EnumVicissitude.VICISSITUDE_POWER_3_NAME, Description = EnumVicissitude.VICISSITUDE_POWER_3_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumVicissitude.VICISSITUDE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumVicissitude.VICISSITUDE_POWER_4_KEY, PowerName = EnumVicissitude.VICISSITUDE_POWER_4_NAME, Description = EnumVicissitude.VICISSITUDE_POWER_4_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumVicissitude.VICISSITUDE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumVicissitude.VICISSITUDE_POWER_5_KEY, PowerName = EnumVicissitude.VICISSITUDE_POWER_5_NAME, Description = EnumVicissitude.VICISSITUDE_POWER_5_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumVicissitude.VICISSITUDE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumVicissitude.VICISSITUDE_ELDER_POWER_1_KEY, PowerName = EnumVicissitude.VICISSITUDE_ELDER_POWER_1_NAME, Description = EnumVicissitude.VICISSITUDE_ELDER_POWER_1_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_ELDER_POWER_1_SYSTEM, ExceptionalSuccess = EnumVicissitude.VICISSITUDE_ELDER_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumVicissitude.VICISSITUDE_ELDER_POWER_2_KEY, PowerName = EnumVicissitude.VICISSITUDE_ELDER_POWER_2_NAME, Description = EnumVicissitude.VICISSITUDE_ELDER_POWER_2_DESCRIPTION, System = EnumVicissitude.VICISSITUDE_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumVicissitude.VICISSITUDE_KEY,
                    DisciplineName = EnumVicissitude.VICISSITUDE_NAME,
                    Description = EnumVicissitude.VICISSITUDE_DESCRIPTION,
                    TestScore = EnumVicissitude.VICISSITUDE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void VisceratikaInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumVisceratika.VISCERATIKA_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumVisceratika.VISCERATIKA_POWER_1_KEY, PowerName = EnumVisceratika.VISCERATIKA_POWER_1_NAME, Description = EnumVisceratika.VISCERATIKA_POWER_1_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumVisceratika.VISCERATIKA_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVisceratika.VISCERATIKA_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumVisceratika.VISCERATIKA_POWER_2_KEY, PowerName = EnumVisceratika.VISCERATIKA_POWER_2_NAME, Description = EnumVisceratika.VISCERATIKA_POWER_2_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumVisceratika.VISCERATIKA_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVisceratika.VISCERATIKA_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumVisceratika.VISCERATIKA_POWER_3_KEY, PowerName = EnumVisceratika.VISCERATIKA_POWER_3_NAME, Description = EnumVisceratika.VISCERATIKA_POWER_3_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumVisceratika.VISCERATIKA_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVisceratika.VISCERATIKA_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumVisceratika.VISCERATIKA_POWER_4_KEY, PowerName = EnumVisceratika.VISCERATIKA_POWER_4_NAME, Description = EnumVisceratika.VISCERATIKA_POWER_4_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumVisceratika.VISCERATIKA_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVisceratika.VISCERATIKA_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumVisceratika.VISCERATIKA_POWER_5_KEY, PowerName = EnumVisceratika.VISCERATIKA_POWER_5_NAME, Description = EnumVisceratika.VISCERATIKA_POWER_5_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumVisceratika.VISCERATIKA_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumVisceratika.VISCERATIKA_POWER_5_EXCEPTIONALSUCCESS },
                    new Power { Level = 6, PowerKey = EnumVisceratika.VISCERATIKA_ELDER_POWER_1_KEY, PowerName = EnumVisceratika.VISCERATIKA_ELDER_POWER_1_NAME, Description = EnumVisceratika.VISCERATIKA_ELDER_POWER_1_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_ELDER_POWER_1_SYSTEM },
                    new Power { Level = 6, PowerKey = EnumVisceratika.VISCERATIKA_ELDER_POWER_2_KEY, PowerName = EnumVisceratika.VISCERATIKA_ELDER_POWER_2_NAME, Description = EnumVisceratika.VISCERATIKA_ELDER_POWER_2_DESCRIPTION, System = EnumVisceratika.VISCERATIKA_ELDER_POWER_2_SYSTEM }
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumVisceratika.VISCERATIKA_KEY,
                    DisciplineName = EnumVisceratika.VISCERATIKA_NAME,
                    Description = EnumVisceratika.VISCERATIKA_DESCRIPTION,
                    TestScore = EnumVisceratika.VISCERATIKA_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        #endregion

        #region Thaumaturgie
        private static void SepulcherInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumSepulcher.SEPULCHER_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumSepulcher.SEPULCHER_POWER_1_KEY, PowerName = EnumSepulcher.SEPULCHER_POWER_1_NAME, Description = EnumSepulcher.SEPULCHER_POWER_1_DESCRIPTION, System = EnumSepulcher.SEPULCHER_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumSepulcher.SEPULCHER_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSepulcher.SEPULCHER_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumSepulcher.SEPULCHER_POWER_2_KEY, PowerName = EnumSepulcher.SEPULCHER_POWER_2_NAME, Description = EnumSepulcher.SEPULCHER_POWER_2_DESCRIPTION, System = EnumSepulcher.SEPULCHER_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumSepulcher.SEPULCHER_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSepulcher.SEPULCHER_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumSepulcher.SEPULCHER_POWER_3_KEY, PowerName = EnumSepulcher.SEPULCHER_POWER_3_NAME, Description = EnumSepulcher.SEPULCHER_POWER_3_DESCRIPTION, System = EnumSepulcher.SEPULCHER_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumSepulcher.SEPULCHER_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSepulcher.SEPULCHER_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumSepulcher.SEPULCHER_POWER_4_KEY, PowerName = EnumSepulcher.SEPULCHER_POWER_4_NAME, Description = EnumSepulcher.SEPULCHER_POWER_4_DESCRIPTION, System = EnumSepulcher.SEPULCHER_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumSepulcher.SEPULCHER_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSepulcher.SEPULCHER_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumSepulcher.SEPULCHER_POWER_5_KEY, PowerName = EnumSepulcher.SEPULCHER_POWER_5_NAME, Description = EnumSepulcher.SEPULCHER_POWER_5_DESCRIPTION, System = EnumSepulcher.SEPULCHER_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumSepulcher.SEPULCHER_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSepulcher.SEPULCHER_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumSepulcher.SEPULCHER_KEY,
                    DisciplineName = EnumSepulcher.SEPULCHER_NAME,
                    Description = EnumSepulcher.SEPULCHER_DESCRIPTION,
                    TestScore = EnumSepulcher.SEPULCHER_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void BonesInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumBones.BONES_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumBones.BONES_POWER_1_KEY, PowerName = EnumBones.BONES_POWER_1_NAME, Description = EnumBones.BONES_POWER_1_DESCRIPTION, System = EnumBones.BONES_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumBones.BONES_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBones.BONES_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumBones.BONES_POWER_2_KEY, PowerName = EnumBones.BONES_POWER_2_NAME, Description = EnumBones.BONES_POWER_2_DESCRIPTION, System = EnumBones.BONES_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumBones.BONES_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBones.BONES_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumBones.BONES_POWER_3_KEY, PowerName = EnumBones.BONES_POWER_3_NAME, Description = EnumBones.BONES_POWER_3_DESCRIPTION, System = EnumBones.BONES_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumBones.BONES_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBones.BONES_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumBones.BONES_POWER_4_KEY, PowerName = EnumBones.BONES_POWER_4_NAME, Description = EnumBones.BONES_POWER_4_DESCRIPTION, System = EnumBones.BONES_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumBones.BONES_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBones.BONES_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumBones.BONES_POWER_5_KEY, PowerName = EnumBones.BONES_POWER_5_NAME, Description = EnumBones.BONES_POWER_5_DESCRIPTION, System = EnumBones.BONES_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumBones.BONES_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBones.BONES_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumBones.BONES_KEY,
                    DisciplineName = EnumBones.BONES_NAME,
                    Description = EnumBones.BONES_DESCRIPTION,
                    TestScore = EnumBones.BONES_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void AschesInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumAsches.ASCHES_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumAsches.ASCHES_POWER_1_KEY, PowerName = EnumAsches.ASCHES_POWER_1_NAME, Description = EnumAsches.ASCHES_POWER_1_DESCRIPTION, System = EnumAsches.ASCHES_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAsches.ASCHES_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAsches.ASCHES_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumAsches.ASCHES_POWER_2_KEY, PowerName = EnumAsches.ASCHES_POWER_2_NAME, Description = EnumAsches.ASCHES_POWER_2_DESCRIPTION, System = EnumAsches.ASCHES_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAsches.ASCHES_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAsches.ASCHES_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumAsches.ASCHES_POWER_3_KEY, PowerName = EnumAsches.ASCHES_POWER_3_NAME, Description = EnumAsches.ASCHES_POWER_3_DESCRIPTION, System = EnumAsches.ASCHES_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumAsches.ASCHES_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAsches.ASCHES_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumAsches.ASCHES_POWER_4_KEY, PowerName = EnumAsches.ASCHES_POWER_4_NAME, Description = EnumAsches.ASCHES_POWER_4_DESCRIPTION, System = EnumAsches.ASCHES_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_CHARISMA_KEY), FocusEffect = EnumAsches.ASCHES_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAsches.ASCHES_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumAsches.ASCHES_POWER_5_KEY, PowerName = EnumAsches.ASCHES_POWER_5_NAME, Description = EnumAsches.ASCHES_POWER_5_DESCRIPTION, System = EnumAsches.ASCHES_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumAsches.ASCHES_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumAsches.ASCHES_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumAsches.ASCHES_KEY,
                    DisciplineName = EnumAsches.ASCHES_NAME,
                    Description = EnumAsches.ASCHES_DESCRIPTION,
                    TestScore = EnumAsches.ASCHES_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void MortisInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumMortis.MORTIS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumMortis.MORTIS_POWER_1_KEY, PowerName = EnumMortis.MORTIS_POWER_1_NAME, Description = EnumMortis.MORTIS_POWER_1_DESCRIPTION, System = EnumMortis.MORTIS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumMortis.MORTIS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMortis.MORTIS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumMortis.MORTIS_POWER_2_KEY, PowerName = EnumMortis.MORTIS_POWER_2_NAME, Description = EnumMortis.MORTIS_POWER_2_DESCRIPTION, System = EnumMortis.MORTIS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumMortis.MORTIS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMortis.MORTIS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumMortis.MORTIS_POWER_3_KEY, PowerName = EnumMortis.MORTIS_POWER_3_NAME, Description = EnumMortis.MORTIS_POWER_3_DESCRIPTION, System = EnumMortis.MORTIS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumMortis.MORTIS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMortis.MORTIS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumMortis.MORTIS_POWER_4_KEY, PowerName = EnumMortis.MORTIS_POWER_4_NAME, Description = EnumMortis.MORTIS_POWER_4_DESCRIPTION, System = EnumMortis.MORTIS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_MANIPULATION_KEY), FocusEffect = EnumMortis.MORTIS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMortis.MORTIS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumMortis.MORTIS_POWER_5_KEY, PowerName = EnumMortis.MORTIS_POWER_5_NAME, Description = EnumMortis.MORTIS_POWER_5_DESCRIPTION, System = EnumMortis.MORTIS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.SOCIAL_APPEARANCE_KEY), FocusEffect = EnumMortis.MORTIS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumMortis.MORTIS_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumMortis.MORTIS_KEY,
                    DisciplineName = EnumMortis.MORTIS_NAME,
                    Description = EnumMortis.MORTIS_DESCRIPTION,
                    TestScore = EnumMortis.MORTIS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void BloodInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumBlood.BLOOD_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumBlood.BLOOD_POWER_1_KEY, PowerName = EnumBlood.BLOOD_POWER_1_NAME, Description = EnumBlood.BLOOD_POWER_1_DESCRIPTION, System = EnumBlood.BLOOD_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumBlood.BLOOD_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBlood.BLOOD_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumBlood.BLOOD_POWER_2_KEY, PowerName = EnumBlood.BLOOD_POWER_2_NAME, Description = EnumBlood.BLOOD_POWER_2_DESCRIPTION, System = EnumBlood.BLOOD_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumBlood.BLOOD_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBlood.BLOOD_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumBlood.BLOOD_POWER_3_KEY, PowerName = EnumBlood.BLOOD_POWER_3_NAME, Description = EnumBlood.BLOOD_POWER_3_DESCRIPTION, System = EnumBlood.BLOOD_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumBlood.BLOOD_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBlood.BLOOD_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumBlood.BLOOD_POWER_4_KEY, PowerName = EnumBlood.BLOOD_POWER_4_NAME, Description = EnumBlood.BLOOD_POWER_4_DESCRIPTION, System = EnumBlood.BLOOD_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumBlood.BLOOD_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBlood.BLOOD_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumBlood.BLOOD_POWER_5_KEY, PowerName = EnumBlood.BLOOD_POWER_5_NAME, Description = EnumBlood.BLOOD_POWER_5_DESCRIPTION, System = EnumBlood.BLOOD_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumBlood.BLOOD_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumBlood.BLOOD_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumBlood.BLOOD_KEY,
                    DisciplineName = EnumBlood.BLOOD_NAME,
                    Description = EnumBlood.BLOOD_DESCRIPTION,
                    TestScore = EnumBlood.BLOOD_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ConjurationInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumConjuration.CONJURATION_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumConjuration.CONJURATION_POWER_1_KEY, PowerName = EnumConjuration.CONJURATION_POWER_1_NAME, Description = EnumConjuration.CONJURATION_POWER_1_DESCRIPTION, System = EnumConjuration.CONJURATION_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumConjuration.CONJURATION_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumConjuration.CONJURATION_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumConjuration.CONJURATION_POWER_2_KEY, PowerName = EnumConjuration.CONJURATION_POWER_2_NAME, Description = EnumConjuration.CONJURATION_POWER_2_DESCRIPTION, System = EnumConjuration.CONJURATION_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumConjuration.CONJURATION_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumConjuration.CONJURATION_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumConjuration.CONJURATION_POWER_3_KEY, PowerName = EnumConjuration.CONJURATION_POWER_3_NAME, Description = EnumConjuration.CONJURATION_POWER_3_DESCRIPTION, System = EnumConjuration.CONJURATION_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumConjuration.CONJURATION_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumConjuration.CONJURATION_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumConjuration.CONJURATION_POWER_4_KEY, PowerName = EnumConjuration.CONJURATION_POWER_4_NAME, Description = EnumConjuration.CONJURATION_POWER_4_DESCRIPTION, System = EnumConjuration.CONJURATION_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumConjuration.CONJURATION_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumConjuration.CONJURATION_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumConjuration.CONJURATION_POWER_5_KEY, PowerName = EnumConjuration.CONJURATION_POWER_5_NAME, Description = EnumConjuration.CONJURATION_POWER_5_DESCRIPTION, System = EnumConjuration.CONJURATION_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumConjuration.CONJURATION_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumConjuration.CONJURATION_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumConjuration.CONJURATION_KEY,
                    DisciplineName = EnumConjuration.CONJURATION_NAME,
                    Description = EnumConjuration.CONJURATION_DESCRIPTION,
                    TestScore = EnumConjuration.CONJURATION_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void CorruptionInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumCorruption.CORRUPTION_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumCorruption.CORRUPTION_POWER_1_KEY, PowerName = EnumCorruption.CORRUPTION_POWER_1_NAME, Description = EnumCorruption.CORRUPTION_POWER_1_DESCRIPTION, System = EnumCorruption.CORRUPTION_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumCorruption.CORRUPTION_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCorruption.CORRUPTION_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumCorruption.CORRUPTION_POWER_2_KEY, PowerName = EnumCorruption.CORRUPTION_POWER_2_NAME, Description = EnumCorruption.CORRUPTION_POWER_2_DESCRIPTION, System = EnumCorruption.CORRUPTION_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumCorruption.CORRUPTION_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCorruption.CORRUPTION_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumCorruption.CORRUPTION_POWER_3_KEY, PowerName = EnumCorruption.CORRUPTION_POWER_3_NAME, Description = EnumCorruption.CORRUPTION_POWER_3_DESCRIPTION, System = EnumCorruption.CORRUPTION_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumCorruption.CORRUPTION_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCorruption.CORRUPTION_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumCorruption.CORRUPTION_POWER_4_KEY, PowerName = EnumCorruption.CORRUPTION_POWER_4_NAME, Description = EnumCorruption.CORRUPTION_POWER_4_DESCRIPTION, System = EnumCorruption.CORRUPTION_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumCorruption.CORRUPTION_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCorruption.CORRUPTION_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumCorruption.CORRUPTION_POWER_5_KEY, PowerName = EnumCorruption.CORRUPTION_POWER_5_NAME, Description = EnumCorruption.CORRUPTION_POWER_5_DESCRIPTION, System = EnumCorruption.CORRUPTION_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumCorruption.CORRUPTION_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumCorruption.CORRUPTION_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumCorruption.CORRUPTION_KEY,
                    DisciplineName = EnumCorruption.CORRUPTION_NAME,
                    Description = EnumCorruption.CORRUPTION_DESCRIPTION,
                    TestScore = EnumCorruption.CORRUPTION_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void ElementsInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumElements.ELEMENTS_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumElements.ELEMENTS_POWER_1_KEY, PowerName = EnumElements.ELEMENTS_POWER_1_NAME, Description = EnumElements.ELEMENTS_POWER_1_DESCRIPTION, System = EnumElements.ELEMENTS_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumElements.ELEMENTS_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumElements.ELEMENTS_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumElements.ELEMENTS_POWER_2_KEY, PowerName = EnumElements.ELEMENTS_POWER_2_NAME, Description = EnumElements.ELEMENTS_POWER_2_DESCRIPTION, System = EnumElements.ELEMENTS_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumElements.ELEMENTS_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumElements.ELEMENTS_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumElements.ELEMENTS_POWER_3_KEY, PowerName = EnumElements.ELEMENTS_POWER_3_NAME, Description = EnumElements.ELEMENTS_POWER_3_DESCRIPTION, System = EnumElements.ELEMENTS_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumElements.ELEMENTS_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumElements.ELEMENTS_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumElements.ELEMENTS_POWER_4_KEY, PowerName = EnumElements.ELEMENTS_POWER_4_NAME, Description = EnumElements.ELEMENTS_POWER_4_DESCRIPTION, System = EnumElements.ELEMENTS_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumElements.ELEMENTS_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumElements.ELEMENTS_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumElements.ELEMENTS_POWER_5_KEY, PowerName = EnumElements.ELEMENTS_POWER_5_NAME, Description = EnumElements.ELEMENTS_POWER_5_DESCRIPTION, System = EnumElements.ELEMENTS_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumElements.ELEMENTS_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumElements.ELEMENTS_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumElements.ELEMENTS_KEY,
                    DisciplineName = EnumElements.ELEMENTS_NAME,
                    Description = EnumElements.ELEMENTS_DESCRIPTION,
                    TestScore = EnumElements.ELEMENTS_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void FireInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumFire.FIRE_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumFire.FIRE_POWER_1_KEY, PowerName = EnumFire.FIRE_POWER_1_NAME, Description = EnumFire.FIRE_POWER_1_DESCRIPTION, System = EnumFire.FIRE_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumFire.FIRE_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFire.FIRE_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumFire.FIRE_POWER_2_KEY, PowerName = EnumFire.FIRE_POWER_2_NAME, Description = EnumFire.FIRE_POWER_2_DESCRIPTION, System = EnumFire.FIRE_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumFire.FIRE_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFire.FIRE_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumFire.FIRE_POWER_3_KEY, PowerName = EnumFire.FIRE_POWER_3_NAME, Description = EnumFire.FIRE_POWER_3_DESCRIPTION, System = EnumFire.FIRE_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumFire.FIRE_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFire.FIRE_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumFire.FIRE_POWER_4_KEY, PowerName = EnumFire.FIRE_POWER_4_NAME, Description = EnumFire.FIRE_POWER_4_DESCRIPTION, System = EnumFire.FIRE_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumFire.FIRE_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFire.FIRE_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumFire.FIRE_POWER_5_KEY, PowerName = EnumFire.FIRE_POWER_5_NAME, Description = EnumFire.FIRE_POWER_5_DESCRIPTION, System = EnumFire.FIRE_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumFire.FIRE_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumFire.FIRE_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumFire.FIRE_KEY,
                    DisciplineName = EnumFire.FIRE_NAME,
                    Description = EnumFire.FIRE_DESCRIPTION,
                    TestScore = EnumFire.FIRE_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void SpiritInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumSpirit.SPIRIT_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumSpirit.SPIRIT_POWER_1_KEY, PowerName = EnumSpirit.SPIRIT_POWER_1_NAME, Description = EnumSpirit.SPIRIT_POWER_1_DESCRIPTION, System = EnumSpirit.SPIRIT_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumSpirit.SPIRIT_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSpirit.SPIRIT_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumSpirit.SPIRIT_POWER_2_KEY, PowerName = EnumSpirit.SPIRIT_POWER_2_NAME, Description = EnumSpirit.SPIRIT_POWER_2_DESCRIPTION, System = EnumSpirit.SPIRIT_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumSpirit.SPIRIT_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSpirit.SPIRIT_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumSpirit.SPIRIT_POWER_3_KEY, PowerName = EnumSpirit.SPIRIT_POWER_3_NAME, Description = EnumSpirit.SPIRIT_POWER_3_DESCRIPTION, System = EnumSpirit.SPIRIT_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumSpirit.SPIRIT_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSpirit.SPIRIT_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumSpirit.SPIRIT_POWER_4_KEY, PowerName = EnumSpirit.SPIRIT_POWER_4_NAME, Description = EnumSpirit.SPIRIT_POWER_4_DESCRIPTION, System = EnumSpirit.SPIRIT_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumSpirit.SPIRIT_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSpirit.SPIRIT_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumSpirit.SPIRIT_POWER_5_KEY, PowerName = EnumSpirit.SPIRIT_POWER_5_NAME, Description = EnumSpirit.SPIRIT_POWER_5_DESCRIPTION, System = EnumSpirit.SPIRIT_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumSpirit.SPIRIT_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumSpirit.SPIRIT_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumSpirit.SPIRIT_KEY,
                    DisciplineName = EnumSpirit.SPIRIT_NAME,
                    Description = EnumSpirit.SPIRIT_DESCRIPTION,
                    TestScore = EnumSpirit.SPIRIT_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void TechnologyInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumTechnology.TECHNOLOGY_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumTechnology.TECHNOLOGY_POWER_1_KEY, PowerName = EnumTechnology.TECHNOLOGY_POWER_1_NAME, Description = EnumTechnology.TECHNOLOGY_POWER_1_DESCRIPTION, System = EnumTechnology.TECHNOLOGY_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumTechnology.TECHNOLOGY_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTechnology.TECHNOLOGY_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumTechnology.TECHNOLOGY_POWER_2_KEY, PowerName = EnumTechnology.TECHNOLOGY_POWER_2_NAME, Description = EnumTechnology.TECHNOLOGY_POWER_2_DESCRIPTION, System = EnumTechnology.TECHNOLOGY_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumTechnology.TECHNOLOGY_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTechnology.TECHNOLOGY_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumTechnology.TECHNOLOGY_POWER_3_KEY, PowerName = EnumTechnology.TECHNOLOGY_POWER_3_NAME, Description = EnumTechnology.TECHNOLOGY_POWER_3_DESCRIPTION, System = EnumTechnology.TECHNOLOGY_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumTechnology.TECHNOLOGY_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTechnology.TECHNOLOGY_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumTechnology.TECHNOLOGY_POWER_4_KEY, PowerName = EnumTechnology.TECHNOLOGY_POWER_4_NAME, Description = EnumTechnology.TECHNOLOGY_POWER_4_DESCRIPTION, System = EnumTechnology.TECHNOLOGY_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_WITZ_KEY), FocusEffect = EnumTechnology.TECHNOLOGY_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTechnology.TECHNOLOGY_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumTechnology.TECHNOLOGY_POWER_5_KEY, PowerName = EnumTechnology.TECHNOLOGY_POWER_5_NAME, Description = EnumTechnology.TECHNOLOGY_POWER_5_DESCRIPTION, System = EnumTechnology.TECHNOLOGY_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumTechnology.TECHNOLOGY_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumTechnology.TECHNOLOGY_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumTechnology.TECHNOLOGY_KEY,
                    DisciplineName = EnumTechnology.TECHNOLOGY_NAME,
                    Description = EnumTechnology.TECHNOLOGY_DESCRIPTION,
                    TestScore = EnumTechnology.TECHNOLOGY_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        private static void WeatherInitializer(DataContext context)
        {
            if (!context.Disciplines.Any(x => x.DisciplineKey == EnumWeather.WEATHER_KEY))
            {
                var powers = new List<Power>
                {
                    new Power { Level = 1, PowerKey = EnumWeather.WEATHER_POWER_1_KEY, PowerName = EnumWeather.WEATHER_POWER_1_NAME, Description = EnumWeather.WEATHER_POWER_1_DESCRIPTION, System = EnumWeather.WEATHER_POWER_1_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumWeather.WEATHER_POWER_1_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumWeather.WEATHER_POWER_1_EXCEPTIONALSUCCESS },
                    new Power { Level = 2, PowerKey = EnumWeather.WEATHER_POWER_2_KEY, PowerName = EnumWeather.WEATHER_POWER_2_NAME, Description = EnumWeather.WEATHER_POWER_2_DESCRIPTION, System = EnumWeather.WEATHER_POWER_2_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumWeather.WEATHER_POWER_2_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumWeather.WEATHER_POWER_2_EXCEPTIONALSUCCESS },
                    new Power { Level = 3, PowerKey = EnumWeather.WEATHER_POWER_3_KEY, PowerName = EnumWeather.WEATHER_POWER_3_NAME, Description = EnumWeather.WEATHER_POWER_3_DESCRIPTION, System = EnumWeather.WEATHER_POWER_3_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumWeather.WEATHER_POWER_3_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumWeather.WEATHER_POWER_3_EXCEPTIONALSUCCESS },
                    new Power { Level = 4, PowerKey = EnumWeather.WEATHER_POWER_4_KEY, PowerName = EnumWeather.WEATHER_POWER_4_NAME, Description = EnumWeather.WEATHER_POWER_4_DESCRIPTION, System = EnumWeather.WEATHER_POWER_4_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_INTELLIGENCE_KEY), FocusEffect = EnumWeather.WEATHER_POWER_4_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumWeather.WEATHER_POWER_4_EXCEPTIONALSUCCESS },
                    new Power { Level = 5, PowerKey = EnumWeather.WEATHER_POWER_5_KEY, PowerName = EnumWeather.WEATHER_POWER_5_NAME, Description = EnumWeather.WEATHER_POWER_5_DESCRIPTION, System = EnumWeather.WEATHER_POWER_5_SYSTEM, Focus = context.Focus.FirstOrDefault(x => x.FocusKey == EnumFocus.MENTAL_PERCEPTION_KEY), FocusEffect = EnumWeather.WEATHER_POWER_5_FOCUS_DESCRIPTION, ExceptionalSuccess = EnumWeather.WEATHER_POWER_5_EXCEPTIONALSUCCESS },
                };

                powers.ForEach(power =>
                {
                    context.Powers.Add(power);
                });

                context.Disciplines.Add(new Discipline
                {
                    DisciplineKey = EnumWeather.WEATHER_KEY,
                    DisciplineName = EnumWeather.WEATHER_NAME,
                    Description = EnumWeather.WEATHER_DESCRIPTION,
                    TestScore = EnumWeather.WEATHER_TEST_SCORE,
                    Powers = powers
                });

                context.SaveChanges();
            }
        }

        #endregion
    }
}