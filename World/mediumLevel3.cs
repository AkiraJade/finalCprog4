import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class mediumLevel3 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mediumLevel3 extends World
{

    /**
     * Constructor for objects of class mediumLevel3.
     * 
     */
    enum PlatformState {
        WAITING,      // Platform hasn't started moving yet
        MOVING,       // Platform is moving up
        STOPPED,      // Platform stopped at target position
        REMOVED,       // Platform has been removed from world
        DISAPPEAR,
        SPAWNED,
        APPEAR
    }

    int playerX;
    int playerY;
    Platform p3Up;
    Platform p4Push;
    Platform p5;
    Platform p6;
    Platform p7;
    Platform p8;
    Platform p9;
    Platform p10Push;
    Platform p11;
    PlatformTrap fake1;
    PlatformTrap fake2;
    PlatformState p3UpState;
    PlatformState p4PushState;
    PlatformState p8State;
    PlatformState p10PushState;
    Spike sp1;
    Spike sp2;

    public mediumLevel3()
    {    
        super(750, 400, 1); 
        prepare();
    }
    
    public void prepare(){
        Person player1 = new Person();
        addObject(player1, 30, 150);
        Platform p1 = new Platform(150, 230);
        addObject(p1, 0, 285);
        Platform p2 = new Platform(100, 200);
        addObject(p2, 200, 200);
        p4Push = new Platform (25,100);
        addObject(p4Push, 237, 150);
        fake1 = new PlatformTrap();
        addObject(fake1, 185, 305);
        fake2 = new PlatformTrap();
        addObject(fake2, 215, 305);
         p6 = new Platform(100, 50);
       
        addObject(p6, 200, 380);
         p7 = new Platform(200, 100);
        addObject(p7, 420, 350);
        p9 = new Platform(100, 100);
        addObject(p9, 630, 350);
        p10Push = new Platform (25,150);
        addObject(p10Push, 332, 399); 
        p11 = new Platform (50,10);
        addObject(p11, 730, 270);
        mediumPortal mPor = new mediumPortal(3);
        addObject(mPor, 725, 230);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        platformUp();
        pushToSpike();
        newPlatform();
    }
    
      public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    boolean personJump = false;
    
    public void platformUp(){
        System.out.println(playerY + playerX);
        
        if (playerY <= 70){
            personJump = true;
        }
       if(playerY >= 120 && playerX <= 140 && playerX >= 84 && 
       !personJump && p3UpState != PlatformState.MOVING
       && p3UpState != PlatformState.STOPPED) {
            p3Up = new Platform (80, 10);
            addObject(p3Up, 115, 330); 
            p3UpState = PlatformState.WAITING;
       }
       
       if (p3UpState ==  PlatformState.WAITING){
           p3UpState = PlatformState.MOVING;
       }
       
       if(p3UpState == PlatformState.MOVING){
           p3Up.setLocation(p3Up.getX(), p3Up.getY() - 5);
           
           if(p3Up.getY() <= 105){
               p3UpState = PlatformState.STOPPED;
               p4PushState = PlatformState.WAITING;
           }
       }
       
       
       
    }
    
    boolean horizontalMove = false;
    boolean secondMove = false;
    public void pushToSpike(){
        if(playerX >= 155 && p4PushState == PlatformState.WAITING){
            p4PushState = PlatformState.MOVING;
        }
        
        if(p4PushState == PlatformState.MOVING && !horizontalMove ){
            p4Push.setLocation(p4Push.getX(), p4Push.getY() - 10);
            
            if (p4Push.getY() <= 50){
                p4PushState = PlatformState.STOPPED;
            }
        }
        
        if(p4PushState == PlatformState.STOPPED && !horizontalMove){
            p4PushState = PlatformState.MOVING;
            horizontalMove = true;
        }
        
        if(p4PushState == PlatformState.MOVING && horizontalMove && !secondMove){
            p4Push.setLocation(p4Push.getX() - 6, p4Push.getY());
            if (p4Push.getX() <= 5){
                 Spike sp1 = new Spike();
                addObject(sp1, 17, 162);
                p4PushState = PlatformState.STOPPED;
                          
            }
        }
     
           
        if(playerX >= 75 && playerY <= 82 && p4PushState == PlatformState.STOPPED 
        && p4Push.getX() <= 5){
            p4PushState = PlatformState.MOVING;
            secondMove = true;
           
        }
        
        if(playerX >= 75 && playerY <= 82 && secondMove && p4PushState == PlatformState.MOVING){
            p4Push.setLocation(p4Push.getX() + 7, p4Push.getY());
            
            if(p4Push.getX() >= 230){
                p4PushState = PlatformState.STOPPED;
                sp1 = new Spike();
                addObject(sp1, 340, 292);
                sp2 = new Spike();
                addObject(sp2, 372, 292);
            }
        }
        
        
    }
    
    boolean secondP10Push = false;
    boolean thirdP10Push = false;
    boolean fourthP10Push = false;
   
    boolean spikeAppear = false;
    public void newPlatform(){
        if(!secondMove && horizontalMove && playerY >= 120 && playerX <= 140 && playerX >= 84){
         p5= new Platform (80, 10);
            addObject(p5, 115, 360); 
        }
        
        if(playerX >= 220 && playerY >= 335 && !secondMove){
             p8 = new Platform (80, 50);
        addObject(p8, 290, 399);
        p8State = PlatformState.SPAWNED;
        
        }
        
        if (playerX >= 285 && p8State == PlatformState.SPAWNED){
            Spike sp2 = new Spike();
            addObject (sp2, 300, 366);
        }
        
         if (playerX >= 550 && playerX < 630 && !fourthP10Push){
            Spike sp3 = new Spike();
            addObject (sp3, 595, 292);
            spikeAppear = true;
        }
        
        if(playerX >=  450 && p10PushState != PlatformState.STOPPED){
            p10PushState = PlatformState.WAITING;
        }
        
        if(p10PushState == PlatformState.WAITING){
           p10PushState = PlatformState.MOVING;
        }
        
        if(p10PushState == PlatformState.MOVING && p10Push.getY() >= 235 && !thirdP10Push){
             p10Push.setLocation(p10Push.getX(), p10Push.getY() - 10);
             
             if(p10Push.getY() <= 235){
                 p10PushState = PlatformState.STOPPED;
             }
        }
        
        if(p10PushState == PlatformState.STOPPED){
            p10PushState = PlatformState.MOVING;
            secondP10Push = true;
        }
        
        if(p10PushState == PlatformState.MOVING && secondP10Push 
        && p10Push.getX() <= 460 && !thirdP10Push){
            p10Push.setLocation(p10Push.getX() + 10, p10Push.getY());
            
              
             if(p10Push.getX() >= 460){
                 p10PushState = PlatformState.STOPPED;
             }
        }
        
        if (p10Push.getX() >= 460 && p10PushState == PlatformState.STOPPED){
            p10PushState = PlatformState.MOVING;
            thirdP10Push = true;
        }
        
        if( thirdP10Push && p10PushState == PlatformState.MOVING){
            p10Push.setLocation(p10Push.getX(), p10Push.getY() + 10);
              if(p10Push.getY() >= 399){
                 p10PushState = PlatformState.STOPPED;
             }
        }
        
          
        
        if(thirdP10Push && p10PushState == PlatformState.STOPPED 
        && p10Push.getX() <= 550 && !spikeAppear  ) {
            p10PushState = PlatformState.MOVING;
            fourthP10Push = true;
        }
        
      
        if(fourthP10Push && p10PushState == PlatformState.MOVING){
             p10Push.setLocation(p10Push.getX() + 10, p10Push.getY());
             
             if(p10Push.getX() >= 550){
                  p10PushState = PlatformState.STOPPED;
             }
        }
        
    }
        
        
    }
    
   