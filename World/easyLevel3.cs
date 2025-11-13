import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class easyLevel3 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class easyLevel3 extends World
{

    /**
     * Constructor for objects of class easyLevel3.
     * 
     */
    Platform p1;
    Platform p2;
    Platform p3;
    boolean moveBlock2 = false;
    int playerX;
    int spikeCount = 0;
    int spikeX = 75;
    int spawnTimer = 0;
    int spawnDelay = 2;
    
    public easyLevel3()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1);
        prepare();
    }
    
    public void prepare(){
        Platform p = new Platform(400,100);
        addObject(p, 200, 350);
        Person player1 = new Person();
        addObject(player1, 30, 280);
        p1 = new Platform (75, 100);
        addObject(p1, 435,350);
        p2 = new Platform(75,100);
        addObject (p2, 510, 350);
        p2.getImage().setTransparency(1);
        easyPortal ePor = new easyPortal(3);
        addObject(ePor, 704, 270);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
        
    }
    
    boolean moveBlock = false;
    
    public void act(){
        downBlock();
        animateBlock();
        spikeChain();
        showPlatform();
    }
    
    public void downBlock(){
        java.util.List<Person>players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        
        if (playerX >= 410) {
           moveBlock = true;
        }
        
    }
    
    boolean spikeAppeared = false;
    boolean downBlock = false;
    public void animateBlock() {
        if(moveBlock) {
             
             p1.setLocation(p1.getX(), p1.getY() + 2);
             
             if(p1.getY() >= 399) {
                 downBlock = true;
             }
        }
        
        
    }
    
     int transparency = 0;
     boolean push = false;
     int transDelay = 2;
     int counterDelay = 0;
     boolean p3NotCreated = false;
    public void showPlatform() {
        
        if(downBlock) {
            counterDelay++;
        if (counterDelay >= transDelay) {
            
          if (p2.getImage().getTransparency() < 250) {
                p2.getImage().setTransparency(transparency);
                transparency+=50;
          }
                
          
                counterDelay = 0;
            }
        }
        
          if (p2.getImage().getTransparency() >= 240 && !p3NotCreated) {
                p3 = new Platform(25,100);
                addObject (p3, 485, 360);
                p3NotCreated = true;
          }
          
        
      
    }
    
    boolean goingDown = false;
    boolean wentDown = false;
    
    public void spikeChain() {
         if (p1.getY() >= 399) {
           
            Spike sp1 = new Spike();
            addObject(sp1, 423, 343);
            Spike sp2 = new Spike();
            addObject(sp2, 442, 343);
         
            spikeAppeared = true;
        }
        
         if (spikeCount < 53 && spikeAppeared && playerX <= 405) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay){
                addObject(new Spike(), spikeX, 292);
                spikeX += 6;
                spawnTimer = 0;
                spikeCount++;
            }
        } 
        
        
            if (playerX >= 505 && p3 != null){
                moveBlock2 = true;
            }
            
            if (moveBlock2 && !push && p3 != null && !goingDown) {
               
                p3.setLocation(p3.getX(), p3.getY() - 10);
                if(p3.getY() <= 250) {
                    push = true;
                
            }
            }
            
            if (push) {
                p3.setLocation(p3.getX() + 10, p3.getY());
            }
            
         if(p3 != null) {
                if(p3.getX() >= 535 && moveBlock2) {
                goingDown = true;
                push = false;
                
            }
    
          if(goingDown){
                 p3.setLocation(p3.getX(), p3.getY() + 10);
                 if(p3.getY() >= 399) {
                     wentDown = true;
                       Platform p4 = new Platform(170, 100);
                       addObject(p4, 670, 350);
                       goingDown = false;
                       
                 }
                 
          }
}
            
            
                
        
    }
    
    
}