import greenfoot.*;

public class backButton extends Actor
{
    public backButton() {
        GreenfootImage img = getImage();
        img.scale(100, 40);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            goBack();
        }
    }
    
    private void goBack()
    {
        World currentWorld = getWorld();
        
        // From Easy levels → go to categoryEasy
        if (currentWorld instanceof easyLevel1 || currentWorld instanceof easyLevel2 ||
            currentWorld instanceof easyLevel3 || currentWorld instanceof easyLevel4 ||
            currentWorld instanceof easyLevel5)
        {
            Greenfoot.setWorld(new categoryEasy());
        }
        // From Medium levels → go to categoryMedium
        else if (currentWorld instanceof mediumLevel1 || currentWorld instanceof mediumLevel2 ||
                 currentWorld instanceof mediumLevel3 || currentWorld instanceof mediumLevel4 ||
                 currentWorld instanceof mediumLevel5)
        {
            Greenfoot.setWorld(new categoryMedium());
        }
        // From Hard levels → go to categoryHard
        else if (currentWorld instanceof ohardLevel1 || currentWorld instanceof ohardLevel2 ||
                 currentWorld instanceof ohardLevel3 || currentWorld instanceof ohardLevel4 ||
                 currentWorld instanceof ohardLevel5)
        {
            Greenfoot.setWorld(new categoryHard());
        }
        // From Advance levels → go to categoryAdvance
        else if (currentWorld instanceof pAdvLevel1 || currentWorld instanceof pAdvLevel2 ||
                 currentWorld instanceof pAdvLevel3 || currentWorld instanceof pAdvLevel4 ||
                 currentWorld instanceof pAdvLevel5)
        {
            Greenfoot.setWorld(new categoryAdvance());
        }
        // From category screens → go to mainMenuP
        else if (currentWorld instanceof categoryEasy || currentWorld instanceof categoryMedium ||
                 currentWorld instanceof categoryHard || currentWorld instanceof categoryAdvance)
        {
            Greenfoot.setWorld(new amainMenuP());
        }
        // From mainMenuP → go to mainMenu
        else if (currentWorld instanceof amainMenuP)
        {
            Greenfoot.setWorld(new amainMenu());
        }
        // From charSelectionBg → go to mainMenu
        else if (currentWorld instanceof charSelectionBg)
        {
            Greenfoot.setWorld(new amainMenu());
        }
        // Default → go to mainMenu
        else
        {
            Greenfoot.setWorld(new amainMenu());
        }
    }
}