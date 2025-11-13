import greenfoot.*;

public class categoryEasy extends World {
    public categoryEasy() {    
        super(750, 400, 1); 
        prepare();
    }
    
    private void prepare() {
        GreenfootImage bg = new GreenfootImage("mainMenuBg.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        
        // Add back button
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 390);
        
        // Add 5 portals at fixed positions
        easyPortal portal1 = new easyPortal(1);
        addObject(portal1, 150, 120);
        
        easyPortal portal2 = new easyPortal(2);
        addObject(portal2, 400, 180);
        
        easyPortal portal3 = new easyPortal(3);
        addObject(portal3, 600, 100);
        
        easyPortal portal4 = new easyPortal(4);
        addObject(portal4, 250, 280);
        
        easyPortal portal5 = new easyPortal(5);
        addObject(portal5, 550, 300);
    }
}