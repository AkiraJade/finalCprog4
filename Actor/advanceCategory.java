import greenfoot.*;

public class advanceCategory extends Actor
{
    private boolean isLocked;
    
    public advanceCategory() {
        updateImage();
    }
    
    private void updateImage() {
        // Advance is locked until Hard category is complete
        isLocked = !LevelProgress.isHardCategoryComplete();
        
        GreenfootImage img = getImage(); 
        img.scale(170, 50);
        
        if (isLocked) {
            img.setTransparency(100); // Make it semi-transparent
        }
        
        setImage(img);
    }
    
    public void act()
    {
        updateImage();
        
        if (Greenfoot.mouseClicked(this))
        {
            if (isLocked) {
                getWorld().showText("Complete all Hard levels first!", getWorld().getWidth()/2, 50);
            } else {
                Greenfoot.setWorld(new categoryAdvance());
            }
        }
    }
}