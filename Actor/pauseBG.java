import greenfoot.*;

public class pauseBG extends Actor
{
    public pauseBG() {
        // Create a semi-transparent dark overlay
        GreenfootImage img = new GreenfootImage(750, 400);
        img.setColor(new Color(0, 0, 0, 180));
        img.fillRect(0, 0, 750, 400);
        
        // Add "PAUSED" text
        img.setColor(Color.WHITE);
        img.setFont(new Font("Arial", true, false, 50));
        img.drawString("PAUSED", 280, 100);
        
        setImage(img);
    }
    
    public void act()
    {
        // Do nothing - just display
    }
}