import greenfoot.*;

public class categoryHard extends World {
    public categoryHard() {    
        super(750, 400, 1); 
        prepare();
    }
    
    private void prepare() {
        GreenfootImage bg = new GreenfootImage("mainMenuBg.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 390);
        
        // Add 5 portals at fixed positions
        hardPortal portal1 = new hardPortal(1);
        addObject(portal1, 300, 100);
        c
        hardPortal portal2 = new hardPortal(2);
        addObject(portal2, 550, 150);
        
        hardPortal portal3 = new hardPortal(3);
        addObject(portal3, 180, 220);
        
        hardPortal portal4 = new hardPortal(4);
        addObject(portal4, 450, 280);
        
        hardPortal portal5 = new hardPortal(5);
        addObject(portal5, 650, 320);
    }
}