import greenfoot.*;

public class retryButton extends Actor
{
    public retryButton() {
        GreenfootImage img = getImage();
        img.scale(150, 50);
        setImage(img);
    }
    
    public void act()
    {
        if (Greenfoot.mouseClicked(this))
        {
            World currentWorld = getWorld();
            World newWorld = null;
            
            // Easy Levels
            if (currentWorld instanceof easyLevel1) newWorld = new easyLevel1();
            else if (currentWorld instanceof easyLevel2) newWorld = new easyLevel2();
            else if (currentWorld instanceof easyLevel3) newWorld = new easyLevel3();
            else if (currentWorld instanceof easyLevel4) newWorld = new easyLevel4();
            else if (currentWorld instanceof easyLevel5) newWorld = new easyLevel5();
            
            // Medium Levels
            else if (currentWorld instanceof mediumLevel1) newWorld = new mediumLevel1();
            else if (currentWorld instanceof mediumLevel2) newWorld = new mediumLevel2();
            else if (currentWorld instanceof mediumLevel3) newWorld = new mediumLevel3();
            else if (currentWorld instanceof mediumLevel4) newWorld = new mediumLevel4();
            else if (currentWorld instanceof mediumLevel5) newWorld = new mediumLevel5();
            
            // Hard Levels
            else if (currentWorld instanceof ohardLevel1) newWorld = new ohardLevel1();
            else if (currentWorld instanceof ohardLevel2) newWorld = new ohardLevel2();
            else if (currentWorld instanceof ohardLevel3) newWorld = new ohardLevel3();
            else if (currentWorld instanceof ohardLevel4) newWorld = new ohardLevel4();
            else if (currentWorld instanceof ohardLevel5) newWorld = new ohardLevel5();
            
            // Advance Levels
            else if (currentWorld instanceof pAdvLevel1) newWorld = new pAdvLevel1();
            else if (currentWorld instanceof pAdvLevel2) newWorld = new pAdvLevel2();
            else if (currentWorld instanceof pAdvLevel3) newWorld = new pAdvLevel3();
            else if (currentWorld instanceof pAdvLevel4) newWorld = new pAdvLevel4();
            else if (currentWorld instanceof pAdvLevel5) newWorld = new pAdvLevel5();
            
            if (newWorld != null) {
                Greenfoot.setWorld(newWorld);
            }
        }
    }
}