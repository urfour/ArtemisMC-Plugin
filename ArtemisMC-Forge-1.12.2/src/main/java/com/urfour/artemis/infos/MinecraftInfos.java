package com.urfour.artemis.infos;

import net.minecraft.client.Minecraft;
import net.minecraft.client.entity.EntityPlayerSP;
import net.minecraft.client.gui.*;
import net.minecraft.client.gui.inventory.GuiInventory;
import net.minecraft.client.multiplayer.WorldClient;
import net.minecraft.init.MobEffects;
import net.minecraft.item.ItemStack;
import net.minecraft.potion.Potion;
import net.minecraft.potion.PotionEffect;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class MinecraftInfos {
    private static final Logger LOG = LogManager.getLogger("artemis-infos");
    private PlayerInfos Player = new PlayerInfos();
    private WorldInfos World = new WorldInfos();
    private GUIInfos Gui = new GUIInfos();
    public void update() {
        Player.getInfos();
        World.getInfos();
        Gui.getInfos();
    }
    private static class PlayerInfos {
        private boolean InGame;
        private float Health;
        private float MaxHealth;
        private float Absorption;
        private boolean IsDead;
        private int ArmorPoints;
        private int ExperienceLevel;
        private float Experience;
        private int FoodLevel;
        private float SaturationLevel;
        private boolean IsSneaking;
        private boolean IsRidingHorse;
        private boolean IsBurning;
        private boolean IsInWater;
        private HashMap<String, Boolean> PlayerEffects = new HashMap<>();
        private static final HashMap<String, Potion> TARGET_EFFECTS;
        private HashMap<String, String> Armor = new HashMap<>();
        private String LeftHandItem;
        private String RightHandItem;
        private int CurrentHotbarSlot;

        static {
            TARGET_EFFECTS = new HashMap<>();
            TARGET_EFFECTS.put("moveSpeed", Potion.getPotionById(1));
            TARGET_EFFECTS.put("moveSlowdown", Potion.getPotionById(2));
            TARGET_EFFECTS.put("haste", Potion.getPotionById(3));
            TARGET_EFFECTS.put("miningFatigue", Potion.getPotionById(4));
            TARGET_EFFECTS.put("strength", Potion.getPotionById(5));
            TARGET_EFFECTS.put("instantHealth", Potion.getPotionById(6));
            TARGET_EFFECTS.put("instantDamage", Potion.getPotionById(7));
            TARGET_EFFECTS.put("jumpBoost", Potion.getPotionById(8));
            TARGET_EFFECTS.put("confusion", Potion.getPotionById(9));
            TARGET_EFFECTS.put("regeneration", Potion.getPotionById(10));
            TARGET_EFFECTS.put("resistance", Potion.getPotionById(11));
            TARGET_EFFECTS.put("fireResistance", Potion.getPotionById(12));
            TARGET_EFFECTS.put("waterBreathing", Potion.getPotionById(13));
            TARGET_EFFECTS.put("invisibility", Potion.getPotionById(14));
            TARGET_EFFECTS.put("blindness", Potion.getPotionById(15));
            TARGET_EFFECTS.put("nightVision", Potion.getPotionById(16));
            TARGET_EFFECTS.put("hunger", Potion.getPotionById(17));
            TARGET_EFFECTS.put("weakness", Potion.getPotionById(18));
            TARGET_EFFECTS.put("poison", Potion.getPotionById(19));
            TARGET_EFFECTS.put("wither", Potion.getPotionById(20));
            TARGET_EFFECTS.put("healthBoost", Potion.getPotionById(21));
            TARGET_EFFECTS.put("absorption", Potion.getPotionById(22));
            TARGET_EFFECTS.put("saturation", Potion.getPotionById(23));
            TARGET_EFFECTS.put("glowing", Potion.getPotionById(24));
            TARGET_EFFECTS.put("levitation", Potion.getPotionById(25));
            TARGET_EFFECTS.put("luck", Potion.getPotionById(26));
            TARGET_EFFECTS.put("badLuck", Potion.getPotionById(27));
            TARGET_EFFECTS.put("slowFalling", null);
            TARGET_EFFECTS.put("conduitPower", null);
            TARGET_EFFECTS.put("dolphinsGrace", null);
            TARGET_EFFECTS.put("bad_omen", null);
            TARGET_EFFECTS.put("villageHero", null);
        }
        private String testIfAir(ItemStack item) {
            if (item.isEmpty()) {
                return null;
            }
            else {
                return item.getDisplayName();
            }
        }
        private void getInfos() {
            try {
                EntityPlayerSP player = Minecraft.getMinecraft().player;
                assert player != null;
                Health = player.getHealth();
                MaxHealth = player.getMaxHealth();
                Absorption = player.getAbsorptionAmount();
                IsDead = player.isDead;
                ArmorPoints = player.getTotalArmorValue();
                ExperienceLevel = player.experienceLevel;
                Experience = player.experience;
                FoodLevel = player.getFoodStats().getFoodLevel();
                SaturationLevel = player.getFoodStats().getSaturationLevel();
                IsSneaking = player.isSneaking();
                IsRidingHorse = player.isRidingHorse();
                IsBurning = player.isBurning();
                IsInWater = player.isInWater();
                for (Map.Entry<String, Potion> effect : TARGET_EFFECTS.entrySet())
                    PlayerEffects.put(effect.getKey(), player.isPotionActive(effect.getValue()));
                ArrayList<String> armorItems = new ArrayList<>();
                for (ItemStack item : player.inventory.armorInventory) {
                    armorItems.add(testIfAir(item));
                }
                Armor.put("Boots", armorItems.get(0));
                Armor.put("Leggings", armorItems.get(1));
                Armor.put("Chestplate", armorItems.get(2));
                Armor.put("Helmet", armorItems.get(3));
                LeftHandItem = testIfAir(player.getHeldItemMainhand());
                RightHandItem = testIfAir(player.getHeldItemOffhand());
                CurrentHotbarSlot = player.inventory.currentItem;
                InGame = true;
            } catch (Exception ex) {
                InGame = false;
            }
        }
    }

    private static class WorldInfos {
        private long WorldTime;
        private boolean IsDayTime;
        private boolean IsRaining;
        private float RainStrength;
        private String Dimension;
        private String Biome;

        private void getInfos() {
            try {
                WorldClient world = Minecraft.getMinecraft().world;
                WorldTime = world.getWorldTime();
                IsDayTime = world.isDaytime();
                IsRaining = world.isRaining();
                RainStrength = world.rainingStrength;
                Dimension = world.provider.getDimensionType().getName();
                Biome = world.getBiome(Minecraft.getMinecraft().player.getPosition()).getBiomeName();
            } catch (Exception ex) {

            }
        }
    }

    private static class GUIInfos {
        private class KeyCode {
            public String code;
            public String context;

            public KeyCode(String code, String context) {
                this.code = code;
                this.context = context;
            }
        }
        private boolean OptionsGuiOpen;
        private boolean InventoryGuiOpen;
        private boolean ChatGuiOpen;
        private boolean KeybindsGuiOpen;
        private boolean PauseGuiOpen;
        private boolean DebugGuiOpen;
        private boolean F3GuiOpen;
        private boolean AdvancementsGuiOpen;
        private boolean RecipeGuiOpen;
        private KeyCode[] Keys;

        private void getInfos() {
            try {
                Minecraft client = Minecraft.getMinecraft();
                OptionsGuiOpen = client.currentScreen instanceof GuiOptions;
                InventoryGuiOpen = client.currentScreen instanceof GuiInventory;
                ChatGuiOpen = client.currentScreen instanceof GuiChat;
                KeybindsGuiOpen = client.currentScreen instanceof GuiControls;
                PauseGuiOpen = client.currentScreen instanceof GuiIngameMenu;
                DebugGuiOpen = client.gameSettings.showDebugInfo;
                F3GuiOpen = client.gameSettings.showDebugInfo;
                AdvancementsGuiOpen = client.currentScreen != null && client.currentScreen.getClass().getName().equals("net.minecraft.client.gui.advancements.GuiScreenAdvancements");
                RecipeGuiOpen = client.currentScreen != null && client.currentScreen.getClass().getName().equals("net.minecraft.client.gui.recipebook.GuiRecipeBook");
                Keys = new KeyCode[client.gameSettings.keyBindings.length];
                for (int i = 0; i < client.gameSettings.keyBindings.length; i++) {
                    Keys[i] = new KeyCode(client.gameSettings.keyBindings[i].getKeyCode() + "", client.gameSettings.keyBindings[i].getKeyDescription());
                }
            } catch (Exception ex) {

            }
        }
    }
}
