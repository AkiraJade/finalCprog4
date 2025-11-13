import greenfoot.*;

public class mediumCategory extends Actor
{
    private boolean isLocked;
    
    public mediumCategory() {
        updateImage();
    }
    
    private void updateImage() {
        // Medium is locked until Easy category is complete
        isLocked = !LevelProgress.isEasyCategoryComplete();
        
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
                getWorld().showText("Complete all Easy levels first!", getWorld().getWidth()/2, 50);
            } else {
                Greenfoot.setWorld(new categoryMedium());
            }
        }
    }
}